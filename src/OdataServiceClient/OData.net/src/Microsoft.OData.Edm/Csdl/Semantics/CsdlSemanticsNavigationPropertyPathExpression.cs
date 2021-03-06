//---------------------------------------------------------------------
// <copyright file="CsdlSemanticsNavigationPropertyPathExpression.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
    /// <summary>
    /// Provides semantics for a Csdl Navigation Property Path expression.
    /// </summary>
    internal class CsdlSemanticsNavigationPropertyPathExpression : CsdlSemanticsPathExpression
    {
        public CsdlSemanticsNavigationPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
            : base(expression, bindingContext, schema)
        {
        }

        public override EdmExpressionKind ExpressionKind
        {
            get { return EdmExpressionKind.NavigationPropertyPath; }
        }
    }
}
