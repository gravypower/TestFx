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

namespace TestFx.FakeItEasy
{
  [AttributeUsage (AttributeTargets.Field)]
  [MeansImplicitUse (ImplicitUseKindFlags.Assign)]
  public abstract class FakeBaseAttribute : Attribute
  {
  }

  [UsedImplicitly]
  public sealed class DummyAttribute : FakeBaseAttribute
  {
  }

  [UsedImplicitly]
  public sealed class FakedAttribute : FakeBaseAttribute
  {
  }

  [MeansImplicitUse (ImplicitUseKindFlags.Access)]
  [UsedImplicitly]
  public sealed class ReturnedFromAttribute : Attribute
  {
    private readonly string _fakeField;

    public ReturnedFromAttribute (string fakeField)
    {
      _fakeField = fakeField;
    }

    public string FakeField
    {
      get { return _fakeField; }
    }
  }
}