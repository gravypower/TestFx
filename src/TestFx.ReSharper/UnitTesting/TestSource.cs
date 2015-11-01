﻿// Copyright 2015, 2014 Matthias Koch
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.UnitTestFramework;
using JetBrains.Util;
using TestFx.ReSharper.UnitTesting.Explorers;

namespace TestFx.ReSharper.UnitTesting
{
  public interface ITestSource : IUnitTestElementsSource
  {
  }

  [SolutionComponent]
  public class TestSource : ITestSource
  {
    private readonly ITestMetadataExplorer _testMetadataExplorer;
    private readonly ITestFileExplorer _testFileExplorer;
    private readonly IMetadataElementsSource _metadataElementsSource;
    private readonly ITestProvider _testProvider;

    public TestSource (
        ITestMetadataExplorer testMetadataExplorer,
        ITestFileExplorer testFileExplorer,
        IMetadataElementsSource metadataElementsSource,
        ITestProvider testProvider)
    {
      _testMetadataExplorer = testMetadataExplorer;
      _testFileExplorer = testFileExplorer;
      _metadataElementsSource = metadataElementsSource;
      _testProvider = testProvider;
    }

    public void ExploreSolution (IUnitTestElementsObserver observer)
    {
    }

      public void ExploreProjects (IDictionary<IProject, FileSystemPath> projects, MetadataLoader loader, IUnitTestElementsObserver observer, CancellationToken cancellationToken)
      {
            _metadataElementsSource.ExploreProjects(projects, loader, observer, _testMetadataExplorer.Explore);
            observer.OnCompleted();
        }

    

    public void ExploreFile (IFile psiFile, IUnitTestElementsObserver observer, Func<bool> interrupted)
    {
      _testFileExplorer.Explore(psiFile, observer.OnUnitTestElementDisposition, () => !interrupted());
      observer.OnCompleted();
    }

    public IUnitTestProvider Provider
    {
      get { return _testProvider; }
    }
  }
}