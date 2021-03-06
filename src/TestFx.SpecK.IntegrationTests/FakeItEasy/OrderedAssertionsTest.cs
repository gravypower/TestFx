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
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TestFx.Evaluation.Results;
using TestFx.FakeItEasy;

namespace TestFx.SpecK.IntegrationTests.FakeItEasy
{
  public class OrderedAssertionsTest : TestBase<OrderedAssertionsTest.DomainSpec>
  {
    [Subject (typeof (OrderedAssertionsTest), "Test")]
    public class DomainSpec : Spec<DomainType>
    {
      [Faked] IDisposable FirstDisposable;
      [Faked] IDisposable SecondDisposable;

      public DomainSpec ()
      {
        Specify (x => x.DoSomething ())
            .DefaultCase (_ => _
                .ItCallsInOrder ("first and second disposable", x =>
                {
                  A.CallTo (() => FirstDisposable.Dispose ()).MustHaveHappened ();
                  A.CallTo (() => SecondDisposable.Dispose ()).MustHaveHappened ();
                }));
      }

      public override DomainType CreateSubject ()
      {
        return new DomainType (FirstDisposable, SecondDisposable);
      }
    }

    [Test]
    public override void Test ()
    {
      AssertTest (Default, State.Failed)
          .WithFailureDetails (
              "calls in order first and second disposable",
              exceptionAssertion: x => x.Name.Should ().Be ("ExpectationException"));
    }

    public class DomainType
    {
      readonly IDisposable _firstDisposable;
      readonly IDisposable _secondDisposable;

      public DomainType (IDisposable FirstDisposable, IDisposable SecondDisposable)
      {
        _firstDisposable = FirstDisposable;
        _secondDisposable = SecondDisposable;
      }

      public void DoSomething ()
      {
        _secondDisposable.Dispose ();
        _firstDisposable.Dispose ();
      }
    }
  }
}