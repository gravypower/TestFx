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
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.UnitTestFramework;
using TestFx.ReSharper.Model.Tree;
using TestFx.ReSharper.Runner.Tasks;
using TestFx.ReSharper.UnitTesting.Utilities;

namespace TestFx.ReSharper.UnitTesting.Elements
{
  public class ChildTestElement : TestElementBase
  {
    public ChildTestElement (ITestIdentity identity, IList<Task> tasks)
        : base(identity, tasks)
    {
    }
        
        //// TODO: introduced invariance!
      public override UnitTestElementNamespace GetNamespace ()
      {
            return Parent != null ? Parent.GetNamespace() : null;
        }

      [CanBeNull]
        public override IDeclaredElement GetDeclaredElement()
      {
          return null;
      }

    [CanBeNull]
    public override IEnumerable<IProjectFile> GetProjectFiles ()
    {
      return Parent != null ? Parent.GetProjectFiles() : Enumerable.Empty<IProjectFile>();
    }

    internal override IEnumerable<ITestFile> GetTestFiles ()
    {
      return Parent != null ? ((TestElementBase) Parent).GetTestFiles() : Enumerable.Empty<ITestFile>();
    }
  }
}