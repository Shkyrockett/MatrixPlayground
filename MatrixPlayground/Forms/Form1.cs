// <copyright file="Form1.cs" company="Shkyrockett" >
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
using System.Windows.Forms;

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Form" />
    public partial class Form1
        : Form
    {
        /// <summary>
        /// The numeric operand1
        /// </summary>
        private NumericFactor numericOperand1;

        /// <summary>
        /// The numeric operand2
        /// </summary>
        private NumericFactor numericOperand2;

        /// <summary>
        /// The numeric resultand
        /// </summary>
        private NumericFactor numericResultand;

        /// <summary>
        /// The fraction operand1
        /// </summary>
        private FractionFactor fractionOperand1;

        /// <summary>
        /// The fraction operand2
        /// </summary>
        private FractionFactor fractionOperand2;

        /// <summary>
        /// The fraction resultand
        /// </summary>
        private FractionFactor fractionResultand;

        /// <summary>
        /// The matrix operand1
        /// </summary>
        private NumericMatrixFactor matrixOperand1;

        /// <summary>
        /// The matrix operand2
        /// </summary>
        private NumericMatrixFactor matrixOperand2;

        /// <summary>
        /// The matrix resultand
        /// </summary>
        private NumericMatrixFactor matrixResultand;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            canvasControl.Font = new Font("Cambria Math", 12);
            canvasControl.BackColor = Color.White;
            //matrixGrid1.RenderBoundaries = true;

            numericOperand1 = new NumericFactor(ConstantFactors.Two);
            numericOperand2 = new NumericFactor(ConstantFactors.NegativeOne);
            fractionOperand1 = new FractionFactor(ConstantFactors.OneHalf);
            fractionOperand2 = new FractionFactor(ConstantFactors.NegativeOneHalf);
            matrixOperand1 = new NumericMatrixFactor(new double[,] {
                { 11, 12, 13, },
                { 21, 22, 23, },
                { 31, 32, 33 }
            }, true);
            matrixOperand2 = new NumericMatrixFactor(new double[,] {
                { 1, 0, 0, },
                { 0, 1, 0, },
                { 0, 0, 1 }
            }, true);
            //matrixOperand2 = new NumericMatrixFactor(new double[,] {
            //    { 11, 12, 13, },
            //    { 21, 22, 23, },
            //    { 31, 32, 33 }
            //}, true);

            listBox1.Items.AddRange(new object[] {
            "AdditionEquationFactory",
            "SubtractionEquationFactory",
            "ProductEquationFactory",
            "QuotientEquationFactory",
            "ScaleEquationFactory",
            "FractionScaleEquationFactory",
            //"LogarithmEquationFactory",
            //"SquareRootEquationFactory",
            //"CubeRootEquationFactory",
            "SquareEquationFactory",
            "CubeEquationFactory",
            "InverseEquationFactory",
            "RotateMatrixClockwiseFactory",
            "RotateMatrixCounterClockwiseFactory",
            "TransposeMatrixEquationFactory"});

            canvasControl.AutoSize = true;
            canvasControl.Focus();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ListBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch ((string)listBox1.SelectedItem)
            {
                case "AdditionEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.AdditionEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand);
                    break;
                case "SubtractionEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.SubtractionEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand);
                    break;
                case "ProductEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.ProductEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand);
                    break;
                case "QuotientEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.QuotientEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand);
                    break;
                case "ScaleEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.ScaleEquationFactory(numericOperand1, matrixOperand2, out matrixResultand);
                    break;
                case "FractionScaleEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.FractionScaleEquationFactory(fractionOperand1, matrixOperand2, out _);
                    break;
                //case "LogarithmEquationFactory":
                //    canvasControl.Expression = SyntaxTemplates.LogarithmEquationFactory(matrixOperand1, out matrixResultand);
                //    break;
                //case "SquareRootEquationFactory":
                //    canvasControl.Expression = SyntaxTemplates.SquareRootEquationFactory(matrixOperand1, out matrixResultand);
                //    break;
                //case "CubeRootEquationFactory":
                //    canvasControl.Expression = SyntaxTemplates.CubeRootEquationFactory(matrixOperand1, out matrixResultand);
                //    break;
                case "SquareEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.SquareEquationFactory(matrixOperand1, out matrixResultand);
                    break;
                case "CubeEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.CubeEquationFactory(matrixOperand1, out matrixResultand);
                    break;
                case "InverseEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.InverseEquationFactory(matrixOperand1, out matrixResultand);
                    break;
                case "RotateMatrixClockwiseFactory":
                    canvasControl.Expression = SyntaxTemplates.RotateMatrixClockwiseFactory(matrixOperand1, out matrixResultand);
                    break;
                case "RotateMatrixCounterClockwiseFactory":
                    canvasControl.Expression = SyntaxTemplates.RotateMatrixCounterClockwiseFactory(matrixOperand1, out matrixResultand);
                    break;
                case "TransposeMatrixEquationFactory":
                    canvasControl.Expression = SyntaxTemplates.TransposeMatrixEquationFactory(matrixOperand1, out matrixResultand);
                    break;
                default:
                    break;
            }

            canvasControl.Invalidate();
        }
    }
}
