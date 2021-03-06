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
using FluentAssertions;
using NUnit.Framework;
using TestFx.Evaluation.Results;

namespace TestFx.SpecK.IntegrationTests.Combinatorics
{
  public class SequencesTest : TestBase<SequencesTest.DomainSpec>
  {
    [Subject (typeof (SequencesTest), "Test")]
    public class DomainSpec : Spec
    {
      int A;
      int B;

      public DomainSpec ()
      {
        Specify (x => A + B)
            .DefaultCase (_ => _
                .WithSequences (
                    "First sequence", new { A = 1, B = 2, Result = 3 },
                    "Second sequence", new { A = 2, B = 3, Result = 6 })
                .Given (x => A = x.Sequence.A)
                .Given (x => B = x.Sequence.B)
                .It ("returns result", x => x.Result.Should ().Be (x.Sequence.Result)));
      }
    }

    [Test]
    public override void Test ()
    {
      AssertTest ("First sequence", State.Passed);
      AssertTest ("Second sequence", State.Failed);
    }
  }
}
