using System.Drawing;
using System.Windows.Forms;

namespace MatrixPlayground
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Form1
        : Form
    {
        private NumberMatrixFactor operand1;

        private NumberMatrixFactor operand2;

        private NumberMatrixFactor resultand;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            matrixGrid1.Font = new Font("Cambria Math", 12);
            matrixGrid1.BackColor = Color.White;
            //matrixGrid1.RenderBoundaries = true;

            //var Coefficient = new CoefficientFactor(2);
            //var fractionCoefficient = new FractionCoefficientFactor(1, 2);
            operand1 = new NumberMatrixFactor(new float[,] {
                { 11, 12, 13, },
                { 21, 22, 23, },
                { 31, 32, 33 } }, true);
            operand2 = new NumberMatrixFactor(new float[,] {
                { 1, 0, 0, },
                { 0, 1, 0, },
                { 0, 0, 1 } }, true);

            //matrixGrid1.Expression = SyntaxTemplates.AdditionEquationFactory(operand1, operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.SubtractionEquationFactory(operand1, operand2, out resultand);
            matrixGrid1.Expression = SyntaxTemplates.ProductEquationFactory(operand1, operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.QuotientEquationFactory(operand1, operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.ScaleEquationFactory(Coefficient, operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.FractionScaleEquationFactory(fractionCoefficient, operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.LogarithmEquationFactory(operand2, out resultand);
            //matrixGrid1.Expression = SyntaxTemplates.CubeRootEquationFactory(operand2, out resultand);

            matrixGrid1.AutoSize = true;
            matrixGrid1.Focus();
        }
    }
}
