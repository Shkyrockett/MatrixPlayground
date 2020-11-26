namespace MatrixPlayground
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private CanvasControl matrixGrid1;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.matrixGrid1 = new MatrixPlayground.CanvasControl();
            this.SuspendLayout();
            // 
            // matrixGrid1
            // 
            this.matrixGrid1.BackColor = System.Drawing.SystemColors.Window;
            this.matrixGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matrixGrid1.Location = new System.Drawing.Point(0, 0);
            this.matrixGrid1.Name = "matrixGrid1";
            this.matrixGrid1.RenderBoundaries = false;
            this.matrixGrid1.Size = new System.Drawing.Size(800, 450);
            this.matrixGrid1.TabIndex = 3;
            this.matrixGrid1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.matrixGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }
        #endregion
    }
}

