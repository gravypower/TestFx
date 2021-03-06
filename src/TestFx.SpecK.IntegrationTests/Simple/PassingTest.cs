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
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TestFx.Evaluation.Results;
using TestFx.Extensibility.Providers;

namespace TestFx.SpecK.IntegrationTests.Simple
{
  public class PassingTest : TestBase<PassingTest.DomainSpec>
  {
    [Subject (typeof (PassingTest), "Test")]
    public class DomainSpec : Spec
    {
      object Object;

      public DomainSpec ()
      {
        Specify (x => Console.WriteLine (true))
            .DefaultCase (_ => _
                .Given (x => { })
                .It ("Assertion", x => { }));
      }
    }

    [Test]
    public override void Test ()
    {
      RunResult.State.Should ().Be (State.Passed);

      var assemblyResult = RunResult.SuiteResults.Single ();
      AssertResult (assemblyResult,
          relativeId: typeof (DomainSpec).Assembly.Location,
          text: typeof (DomainSpec).Assembly.GetName ().Name,
          state: State.Passed);

      var typeResult = assemblyResult.SuiteResults.Single ();
      AssertResult (typeResult,
          relativeId: typeof (DomainSpec).FullName,
          text: "PassingTest.Test",
          state: State.Passed);

      var testResult = typeResult.TestResults.Single ();
      AssertResult (testResult,
          relativeId: "<Default>",
          text: "<Default>",
          state: State.Passed);

      var operationResults = testResult.OperationResults.ToList ();
      AssertResult (operationResults[0], Reset_Instance_Fields, State.Passed, OperationType.Action);
      AssertResult (operationResults[1], "<Arrangement>", State.Passed, OperationType.Action);
      AssertResult (operationResults[2], Action, State.Passed, OperationType.Action);
      AssertResult (operationResults[3], "Assertion", State.Passed, OperationType.Assertion);

      testResult.Identity.Absolute.Should ()
          .EndWith (
              "TestFx.SpecK.IntegrationTests.dll � " +
              "TestFx.SpecK.IntegrationTests.Simple.PassingTest+DomainSpec � " +
              "<Default>");
    }
  }
}