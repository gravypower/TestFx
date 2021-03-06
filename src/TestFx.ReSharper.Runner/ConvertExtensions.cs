﻿// Copyright 2014, 2013 Matthias Koch
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
using JetBrains.ReSharper.TaskRunnerFramework;
using TestFx.Old.OldEval.Reporting;

namespace TestFx.ReSharper.Runner
{
  public static class ConvertExtensions
  {
    public static TaskException ToTaskException (this ExceptionDescriptor exception, string desc)
    {
      return new TaskException("x" + desc + "\r\n" + exception.Type.FullName, desc + "\r\n" + exception.Message, exception.StackTrace);
    }

    public static TaskResult ToTaskResult (this State state)
    {
      switch (state)
      {
        case State.Passed:
          return TaskResult.Success;
        case State.Skipped:
          return TaskResult.Skipped;
        case State.Inconclusive:
        case State.NotImplemented:
          return TaskResult.Inconclusive;
        case State.Failed:
          return TaskResult.Exception;
        default:
          throw new NotSupportedException();
      }
    }
  }
}