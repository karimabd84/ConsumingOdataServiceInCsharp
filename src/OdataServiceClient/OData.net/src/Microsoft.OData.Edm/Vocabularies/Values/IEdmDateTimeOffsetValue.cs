//---------------------------------------------------------------------
// <copyright file="IEdmDateTimeOffsetValue.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace Microsoft.OData.Edm.Vocabularies
{
    /// <summary>
    /// Represents an EDM datetime with offset value.
    /// </summary>
    public interface IEdmDateTimeOffsetValue : IEdmPrimitiveValue
    {
        /// <summary>
        /// Gets the definition of this value.
        /// </summary>
        DateTimeOffset Value { get; }
    }
}
