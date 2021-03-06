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
using JetBrains.Annotations;
using JetBrains.ProjectModel;

namespace TestFx.ReSharper.Model.Metadata.Aggregation
{
  public interface IAssemblyAggregatorFactory
  {
    IAssemblyAggregator Create (IProject project, [CanBeNull] Func<bool> notInterrupted);
  }

  public class AssemblyAggregatorFactory : IAssemblyAggregatorFactory
  {
    public static IAssemblyAggregatorFactory Instance = new AssemblyAggregatorFactory();

    public IAssemblyAggregator Create (IProject project, [CanBeNull] Func<bool> notInterrupted)
    {
      notInterrupted = notInterrupted ?? (() => true);
      return new AssemblyAggregator(new MetadataPresenter(), project, notInterrupted);
    }
  }
}