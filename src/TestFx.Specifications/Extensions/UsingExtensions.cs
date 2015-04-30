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
using TestFx.Extensibility;
using TestFx.Specifications.Implementation;
using TestFx.Specifications.Implementation.Controllers;
using TestFx.Specifications.InferredApi;
using TestFx.Utilities.Reflection;

namespace TestFx.Specifications
{
  public static class UsingExtensions
  {
    public static IArrangeOrAssert<TSubject, TResult, TVars> GivenUsing<TSubject, TResult, TVars> (
        this IArrange<TSubject, TResult, TVars> arrange,
        Type disposableType)
    {
      return arrange.GivenUsing(disposableType.Name, x => disposableType.CreateInstance<IDisposable>());
    }

    public static IArrangeOrAssert<TSubject, TResult, TVars> GivenUsing<TSubject, TResult, TVars, TDisposable> (
        this IArrange<TSubject, TResult, TVars> arrange,
        Func<ITestContext<TSubject, TResult, TVars>, TDisposable> scopeProvider)
        where TDisposable : IDisposable
    {
      return arrange.GivenUsing(typeof (TDisposable).Name, scopeProvider);
    }

    public static IArrangeOrAssert<TSubject, TResult, TVars> GivenUsing<TSubject, TResult, TVars, TDisposable> (
        this IArrange<TSubject, TResult, TVars> arrange,
        string text,
        Func<ITestContext<TSubject, TResult, TVars>, TDisposable> scopeProvider)
        where TDisposable : IDisposable
    {
      var controller = arrange.Get<ITestController<TSubject, TResult, TVars>>();

      IDisposable scope = null;
      controller.AddSetupCleanup<Arrange, CleanupCommon>(
          "Create " + text,
          x => scope = scopeProvider((ITestContext<TSubject, TResult, TVars>) x),
          "Dispose " + text,
          x => scope.Dispose());
      return (IArrangeOrAssert<TSubject, TResult, TVars>) arrange;
    }
  }
}