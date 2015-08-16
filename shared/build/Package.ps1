[CmdletBinding()]
Param(
  [Parameter()] [string] $NuGetDir = $null,
  [Parameter()] [string] $MsBuildDir = $null,
  [Parameter()] [string] $DotCoverDir = $null,
  
  [Parameter()] [string] $Configuration = "Debug",
  [Parameter()] [string] $Targets = "Rebuild",
 
  [Parameter()] [bool] $SkipTests = $false
)

Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"; trap { $host.SetShouldExit(1) }
Import-Module $PSScriptRoot\_Import.ps1

Write-Step "Package"

$TempOutputDir = Join-Path $OutputDir "Temp"
mkdir -Force $TempOutputDir 

$NuSpecFiles | %{
  Exec { & $NuGet pack $_ -Version $SemVer -Properties Configuration=$Configuration -BasePath $SourceDir -OutputDirectory $TempOutputDir -Symbols -NoPackageAnalysis }

  $PackageIdWithoutVersion = ([System.IO.Path]::GetFileNameWithoutExtension($_))
  $PackageIdWithVersion = gci $TempOutputDir -Filter "*.nupkg" | select -First 1 -ExpandProperty BaseName

  gci $TempOutputDir | %{
    mv $_.FullName (Join-Path $OutputDir ("$($_.BaseName -replace $PackageIdWithVersion,$PackageIdWithoutVersion)$($_.Extension)"))
  }
}