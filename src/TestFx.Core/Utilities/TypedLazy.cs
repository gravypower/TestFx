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
using TestFx.Utilities.Reflection;

namespace TestFx.Utilities
{
  public class TypedLazy<T> : Lazy<T>
  {
    private readonly Type _type;

    public TypedLazy (Type type)
        : base(() => type.CreateInstance<T>())
    {
      _type = type;
    }

    public Type Type
    {
      get { return _type; }
    }
  }
}