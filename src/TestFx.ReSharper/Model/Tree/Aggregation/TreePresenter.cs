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
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;
using TestFx.Extensibility;
using TestFx.ReSharper.Utilities.Psi;
using TestFx.Utilities;

namespace TestFx.ReSharper.Model.Tree.Aggregation
{
  public interface ITreePresenter
  {
    [CanBeNull]
    string Present (IClassDeclaration classDeclaration);

    [CanBeNull]
    string Present (IInvocationExpression invocationExpression);
  }

  public class TreePresenter : ITreePresenter
  {
    private readonly IIntrospectionPresenter _introspectionPresenter;

    public TreePresenter ()
    {
      _introspectionPresenter = new IntrospectionPresenter();
    }

    [CanBeNull]
    public string Present (IClassDeclaration classDeclaration)
    {
      var clazz = (IClass) classDeclaration.DeclaredElement;

      var subjectAttributeData = clazz.GetAttributeData<SubjectAttributeBase>();
      if (subjectAttributeData == null)
        return null;

      var subjectAttribute = subjectAttributeData.ToCommon();
      var subjectAttributeClass = subjectAttributeData.GetAttributeType().GetTypeElement<IClass>().AssertNotNull();
      var subjectAttributeConstructor = subjectAttributeClass.Constructors.Single();
      var displayFormatAttribute = subjectAttributeConstructor.GetAttributeData<DisplayFormatAttribute>().ToCommon();
      return _introspectionPresenter.Present(displayFormatAttribute, subjectAttribute);
    }

    [CanBeNull]
    public string Present (IInvocationExpression invocationExpression)
    {
      var method = invocationExpression.Reference.GetResolved<IMethod>();
      if (method == null)
        return null;

      var displayFormatAttributeData = method.GetAttributeData<DisplayFormatAttribute>();
      if (displayFormatAttributeData == null)
        return null;

      var displayFormatAttribute = displayFormatAttributeData.ToCommon();
      var commonExpressions = invocationExpression.Arguments.Select(GetConstantValue);
      return _introspectionPresenter.Present(displayFormatAttribute, commonExpressions);
    }

    private object GetConstantValue (ICSharpArgument argument)
    {
      if (argument.Kind == ParameterKind.UNKNOWN)
        return IntrospectionPresenter.UnknownValue;

      var literalExpression = argument.Value as ILiteralExpression;
      if (literalExpression == null)
        return IntrospectionPresenter.UnknownValue;

      var constantValue = literalExpression.ConstantValue.Value.AssertNotNull();
      return constantValue.ToString().Trim('"');
    }
  }
}