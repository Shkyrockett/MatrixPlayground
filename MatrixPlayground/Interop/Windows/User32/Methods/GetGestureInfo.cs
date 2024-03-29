﻿// <copyright file="GetGestureInfo.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2021 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// 
/// </summary>
internal static partial class Interop
{
    /// <summary>
    /// 
    /// </summary>
    internal static partial class Windows
    {
        /// <summary>
        /// 
        /// </summary>
        internal static partial class User32
        {
            /// <summary>
            /// Gets the gesture information.
            /// </summary>
            /// <param name="hGestureInfo">The h gesture information.</param>
            /// <param name="pGestureInfo">The p gesture information.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            [DllImport(Libraries.User32)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetGestureInfo(IntPtr hGestureInfo, ref GestureInfo pGestureInfo);
        }
    }
}
