//---------------------------------------------------------------------
// <copyright file="GeometryMultiLineString.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.Spatial
{
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>Represents the geometry multi-line string.</summary>
    public abstract class GeometryMultiLineString : GeometryMultiCurve
    {
        /// <summary>Initializes a new instance of the <see cref="Microsoft.Spatial.GeometryMultiLineString" /> class.</summary>
        /// <param name="coordinateSystem">The coordinate system of this instance.</param>
        /// <param name="creator">The implementation that created this instance.</param>
        protected GeometryMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
            : base(coordinateSystem, creator)
        {
        }

        /// <summary>Gets a collection of line strings.</summary>
        /// <returns>A collection of line strings.</returns>
        public abstract ReadOnlyCollection<GeometryLineString> LineStrings { get; }

        /// <summary>Determines whether this instance and another specified geometry instance have the same value.</summary>
        /// <returns>true if the value of the value parameter is the same as this instance; otherwise, false.</returns>
        /// <param name="other">The geometry to compare to this instance.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
        public bool Equals(GeometryMultiLineString other)
        {
            return this.BaseEquals(other) ?? this.LineStrings.SequenceEqual(other.LineStrings);
        }

        /// <summary>Determines whether this instance and the specified object have the same value.</summary>
        /// <returns>true if the value of the value parameter is the same as this instance; otherwise, false.</returns>
        /// <param name="obj">The object to compare to this instance.</param>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as GeometryMultiLineString);
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return Microsoft.Spatial.Geography.ComputeHashCodeFor(this.CoordinateSystem, this.LineStrings);
        }
    }
}