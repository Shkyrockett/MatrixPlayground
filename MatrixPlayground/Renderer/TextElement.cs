// <copyright file="TextElement.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using MathematicsNotationLibrary;
using System.Drawing;

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IRenderable" />
    public class TextElement
        : IRenderable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextElement" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="stringFormat">The string format.</param>
        public TextElement(string text, Font font, Brush? brush, Pen? pen, RectangleF bounds, StringFormat? stringFormat = default)
        {
            Text = text;
            Font = font;
            Brush = brush;
            Pen = pen;
            Bounds = bounds;
            StringFormat = stringFormat;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets the brush.
        /// </summary>
        /// <value>
        /// The brush.
        /// </value>
        public Brush? Brush { get; set; }

        /// <summary>
        /// Gets or sets the pen.
        /// </summary>
        /// <value>
        /// The pen.
        /// </value>
        public Pen? Pen { get; set; }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <value>
        /// The <see cref="RectangleF" />.
        /// </value>
        public RectangleF? Bounds { get; set; }

        /// <summary>
        /// Gets or sets the string format.
        /// </summary>
        /// <value>
        /// The string format.
        /// </value>
        public StringFormat? StringFormat { get; set; }

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public void Draw(Graphics graphics) => Draw(graphics, Brush, Pen);

        /// <summary>
        /// Draws the specified graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pen">The pen.</param>
        public void Draw(Graphics graphics, Brush? brush, Pen? pen)
        {
            if (brush is not null) graphics.DrawString(Text, Font, brush, Bounds?.Location ?? PointF.Empty, StringFormat);
        }
    }
}
