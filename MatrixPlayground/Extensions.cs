// <copyright file="Extensions.cs" company="Shkyrockett" >
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

namespace MatrixPlayground;

/// <summary>
/// 
/// </summary>
public static partial class Extensions
{
    #region Extensions To Add Missing Graphics Methods
    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="rectangle">The rectangle.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, RectangleF rectangle) => graphics.FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="point">The point.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, Point point, Size size) => graphics.FillRectangle(brush, point.X, point.Y, size.Width, size.Height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="point">The point.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, PointF point, SizeF size) => graphics.FillRectangle(brush, point.X, point.Y, size.Width, size.Height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, int x, int y, Size size) => graphics.FillRectangle(brush, x, y, size.Width, size.Height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, float x, float y, SizeF size) => graphics.FillRectangle(brush, x, y, size.Width, size.Height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="point">The point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, Point point, int width, int height) => graphics.FillRectangle(brush, point.X, point.Y, width, height);

    /// <summary>
    /// Fills the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="point">The point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void FillRectangle(this Graphics graphics, Brush brush, PointF point, float width, float height) => graphics.FillRectangle(brush, point.X, point.Y, width, height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="rectangle">The rectangle.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, RectangleF rectangle) => graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="point">The point.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, Point point, Size size) => graphics.DrawRectangle(pen, point.X, point.Y, size.Width, size.Height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="point">The point.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, PointF point, SizeF size) => graphics.DrawRectangle(pen, point.X, point.Y, size.Width, size.Height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, int x, int y, Size size) => graphics.DrawRectangle(pen, x, y, size.Width, size.Height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="size">The size.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, float x, float y, SizeF size) => graphics.DrawRectangle(pen, x, y, size.Width, size.Height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="point">The point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, Point point, int width, int height) => graphics.DrawRectangle(pen, point.X, point.Y, width, height);

    /// <summary>
    /// Draws the rectangle.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="point">The point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void DrawRectangle(this Graphics graphics, Pen pen, PointF point, float width, float height) => graphics.DrawRectangle(pen, point.X, point.Y, width, height);
    #endregion
}
