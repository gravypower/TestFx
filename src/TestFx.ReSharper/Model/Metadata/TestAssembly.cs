// Copyright 2015, 2014 Matthias Koch
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
using System.Diagnostics;
using JetBrains.Metadata.Reader.API;
using TestFx.ReSharper.Model.Metadata.Wrapper;
using TestFx.Utilities;

namespace TestFx.ReSharper.Model.Metadata
{
  public interface ITestAssembly : ITestMetadataHolder, IMetadataAssembly
  {
  }

  [DebuggerDisplay (Identifiable.DebuggerDisplay)]
  public class TestAssembly : MetadataAssemblyBase, ITestAssembly
  {
    private readonly IEnumerable<ITestMetadata> _testMetadatas;

    public TestAssembly (
        IEnumerable<ITestMetadata> testMetadatas,
        IMetadataAssembly metadataAssembly)
        : base(metadataAssembly)
    {
      _testMetadatas = testMetadatas;
    }

    public IEnumerable<ITestMetadata> TestMetadatas
    {
      get { return _testMetadatas; }
    }
  }
}