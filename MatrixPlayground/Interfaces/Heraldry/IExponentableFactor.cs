﻿// <copyright file="IExponentableFactor.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MatrixPlayground.IFactor" />
    public interface IExponentableFactor
        : IFactor
    {
        /// <summary>
        /// Gets or sets the exponent.
        /// </summary>
        /// <value>
        /// The exponent.
        /// </value>
        IExpression? Exponent { get; set; }
    }
}
