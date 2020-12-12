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

using System;
using System.Drawing;
using System.Windows.Forms;
using MathematicsNotationLibrary;

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

        private Func<IExpression> expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //canvasControl.Font = new Font("Cambria Math", 12);
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

            expression = () => new RelationalOperation(ComparisonOperators.Equals, null, null);
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
            expression = ((string)listBox1.SelectedItem) switch
            {
                "AdditionEquationFactory" => () => SyntaxTemplates.AdditionEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand),
                "SubtractionEquationFactory" => () => SyntaxTemplates.SubtractionEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand),
                "ProductEquationFactory" => () => SyntaxTemplates.ProductEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand),
                "QuotientEquationFactory" => () => SyntaxTemplates.QuotientEquationFactory(matrixOperand1, matrixOperand2, out matrixResultand),
                "ScaleEquationFactory" => () => SyntaxTemplates.ScaleEquationFactory(numericOperand1, matrixOperand2, out matrixResultand),
                "FractionScaleEquationFactory" => () => SyntaxTemplates.FractionScaleEquationFactory(fractionOperand1, matrixOperand2, out _),
                //"LogarithmEquationFactory" => () => SyntaxTemplates.LogarithmEquationFactory(matrixOperand1, out matrixResultand),
                //"SquareRootEquationFactory" => () => SyntaxTemplates.SquareRootEquationFactory(matrixOperand1, out matrixResultand),
                //"CubeRootEquationFactory" => () => SyntaxTemplates.CubeRootEquationFactory(matrixOperand1, out matrixResultand),
                "SquareEquationFactory" => () => SyntaxTemplates.SquareEquationFactory(matrixOperand1, out matrixResultand),
                "CubeEquationFactory" => () => SyntaxTemplates.CubeEquationFactory(matrixOperand1, out matrixResultand),
                "InverseEquationFactory" => () => SyntaxTemplates.InverseEquationFactory(matrixOperand1, out matrixResultand),
                "RotateMatrixClockwiseFactory" => () => SyntaxTemplates.RotateMatrixClockwiseFactory(matrixOperand1, out matrixResultand),
                "RotateMatrixCounterClockwiseFactory" => () => SyntaxTemplates.RotateMatrixCounterClockwiseFactory(matrixOperand1, out matrixResultand),
                "TransposeMatrixEquationFactory" => () => SyntaxTemplates.TransposeMatrixEquationFactory(matrixOperand1, out matrixResultand),
                _ => () => new RelationalOperation(ComparisonOperators.Equals, null, null),
            };

            canvasControl.Expression = expression.Invoke();
            canvasControl.Invalidate();
        }

        /// <summary>
        /// Handles the Click event of the CanvasControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CanvasControl_Click(object sender, EventArgs e)
        {
            if (expression is not null) canvasControl.Expression = expression.Invoke();
        }

        /// <summary>
        /// Handles the TextBoxValidated event of the CanvasControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void CanvasControl_TextBoxValidated(object sender, EventArgs e)
        {
            if (expression is not null) canvasControl.Expression = expression.Invoke();
        }
    }
}
