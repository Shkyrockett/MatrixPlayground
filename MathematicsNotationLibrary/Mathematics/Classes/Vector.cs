﻿// <copyright file="GeneralVector.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2021 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <seealso cref="IEquatable{T}" />
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class Vector<T>
    : IEquatable<Vector<T>>, IVector<T>
    where T : INumber<T>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector{T}"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    public Vector(T[] values)
    {
        Items = values;
    }
    #endregion

    /// <summary>
    /// Gets the values.
    /// </summary>
    /// <value>
    /// The values.
    /// </value>
    public T[] Items { get; set; }

    /// <summary>
    /// Gets the number of dimensions.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public int Count => Items.GetLength(0);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Vector<T> left, Vector<T> right) => EqualityComparer<Vector<T>>.Default.Equals(left, right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Vector<T> left, Vector<T> right) => !(left == right);

    /// <summary>
    /// Performs an implicit conversion from <see cref="Array" /> to <see cref="Vector{T}" />.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Vector<T>(T[] array) => ToVector(array);

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj) => obj is Vector<T> v && Equals(v);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Vector<T>? other) => EqualityComparer<T[]>.Default.Equals(Items, other?.Items);

    /// <summary>
    /// Converts to matrix.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector<T> ToVector(T[] array) => new(array);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode() => HashCode.Combine(Items);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        var sb = new StringBuilder();
        sb.Append('{');
        for (var i = 0; i < Count; i++)
        {
            sb.Append($"{Items[i].ToString(format, formatProvider)},\t");
        }

        sb.Append('}');

        return sb.ToString();
    }

    /// <summary>
    /// Gets the debugger display.
    /// </summary>
    /// <returns></returns>
    private string GetDebuggerDisplay() => ToString();
}
