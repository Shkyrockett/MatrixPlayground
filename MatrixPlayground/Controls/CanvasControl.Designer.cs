// <copyright file="CanvasControl.Designer.cs" company="Shkyrockett" >
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
    /// <seealso cref="System.Windows.Forms.UserControl" />
    partial class CanvasControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The numeric up down1
        /// </summary>
        private System.Windows.Forms.NumericUpDown numericUpDown1;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDown1.Location = new System.Drawing.Point(0, 0);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 18, 4, 18);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 26);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            this.numericUpDown1.Validated += new System.EventHandler(this.NumericUpDown1_Validated);
            // 
            // CanvasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 89F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Font = new System.Drawing.Font("Cambria Math", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 18, 4, 18);
            this.Name = "CanvasControl";
            this.Size = new System.Drawing.Size(193, 193);
            this.Load += new System.EventHandler(this.CanvasControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CanvasControl_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CanvasControl_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.CanvasControl_MouseWheel);
            this.Move += new System.EventHandler(this.Canvas_Move);
            this.Resize += new System.EventHandler(this.Canvas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
