// <copyright file="CanvasControl.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class CanvasControl
        : UserControl
    {
        private Dictionary<RectangleF, IExpression> hotSpots = new Dictionary<RectangleF, IExpression>();

        private Action<float> SetCell = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasControl"/> class.
        /// </summary>
        public CanvasControl()
        {
            DoubleBuffered = true;
            InitializeComponent();

            Expression = new RelationalOperation(ComparisonOperators.Equals, null, null);
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public IExpression Expression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [render boundaries].
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if [render boundaries]; otherwise, <see langword="false" />.
        /// </value>
        public bool RenderBoundaries { get; set; }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        //[DefaultValue(System.Drawing.SystemColors.Window)]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        /// <summary>
        /// Handles the Paint event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var window = new SizeF(Size.Width - 1f, Size.Height - 1f);
            var bounds = new RectangleF(PointF.Empty, window);
            e.Graphics.DrawRectangle(Pens.CornflowerBlue, bounds);

            var padding = Math.Min(window.Width * 0.25f, window.Height * 0.25f);
            var outer = new SizeF(window.Width - padding, window.Height - padding);
            var inner = Expression.Dimensions(e.Graphics, Font, 1f);
            var scale = Utilities.FitSizeWithin(inner, outer);
            inner = Expression.Dimensions(e.Graphics, Font, scale);

            PointF location = new((window.Width - inner.Width) * 0.5f, (window.Height - inner.Height) * 0.5f);

            e.Graphics.ResetTransform();
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.CompositingMode = CompositingMode.SourceOver;

            Expression?.Draw(e.Graphics, Font, Brushes.Black, Pens.Black, scale, location, RenderBoundaries);

            CalculateClickRegions();

            using var brush = new SolidBrush(Color.FromArgb(128, Color.CornflowerBlue));
            if (hotSpots is not null && hotSpots.Count > 0)
            {
                foreach (var item in hotSpots.Keys)
                {
                    e.Graphics.FillRectangle(brush, item.X, item.Y, item.Width, item.Height);
                }
            }
        }

        /// <summary>
        /// Handles the Resize event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Canvas_Resize(object sender, EventArgs e) => Invalidate();

        /// <summary>
        /// Handles the Move event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Canvas_Move(object sender, EventArgs e) => Invalidate();

        /// <summary>
        /// Calculates the click regions.
        /// </summary>
        /// <returns></returns>
        public void CalculateClickRegions()
        {
            var expressions = Expression.Expressions();
            hotSpots?.Clear();
            hotSpots = new();
            foreach (var item in expressions)
            {
                if (item is not null && item.Editable && item.Bounds is RectangleF bound && !hotSpots.ContainsKey(bound)) hotSpots.Add(bound, item);
            }
        }

        /// <summary>
        /// Handles the Load event of the CanvasControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void CanvasControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the MouseClick event of the CanvasControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void CanvasControl_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var spot in hotSpots.Keys)
            {
                if (spot.Contains(e.Location))
                {
                    if (SetCell is not null) SetCell((float)numericUpDown1.Value);
                    var cell = (CoefficientFactor)hotSpots[spot];
                    numericUpDown1.Visible = true;
                    numericUpDown1.Value = (decimal)cell.Value;
                    numericUpDown1.Bounds = Rectangle.Truncate(spot);
                    SetCell = (v) => cell.Value = v;
                    Invalidate();
                    return;
                }
            }
            numericUpDown1.Visible = false;
            SetCell = null;
        }

        /// <summary>
        /// Handles the ValueChanged event of the NumericUpDown1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (SetCell is not null)
            {
                SetCell((float)numericUpDown1.Value);
            }
            Invalidate();
        }

        /// <summary>
        /// Handles the Validated event of the NumericUpDown1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void NumericUpDown1_Validated(object sender, EventArgs e)
        {
            SetCell = null;
        }
    }
}
