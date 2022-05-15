// <copyright file="WindowsFormsRenderer.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2021 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

namespace MatrixPlayground;

/// <summary>
/// 
/// </summary>
public class WindowsFormsRenderer
{
    /// <summary>
    /// The graphics
    /// </summary>
    readonly Graphics graphics;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsFormsRenderer"/> class.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    public WindowsFormsRenderer(Graphics graphics)
    {
        this.graphics = graphics;
    }

    /// <summary>
    /// Draws the text.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="font">The font.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="point">The point.</param>
    public void DrawText(string text, Font font, Brush brush, PointF point) => graphics.DrawString(text, font, brush, point);
}
