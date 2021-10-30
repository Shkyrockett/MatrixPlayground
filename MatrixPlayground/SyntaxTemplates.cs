// <copyright file="SyntaxTemplates.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2021 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

using MathematicsNotationLibrary;
using Microsoft.Toolkit.HighPerformance;

namespace MatrixPlayground;

/// <summary>
/// 
/// </summary>
public static class SyntaxTemplates
{
    /// <summary>
    /// Addition equation factory.
    /// </summary>
    /// <param name="matrix1">The matrix1.</param>
    /// <param name="matrix2">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation AdditionEquationFactory(NumericMatrixFactor matrix1, NumericMatrixFactor matrix2, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix1.Coefficients;
        var values2 = matrix2.Coefficients;
        var values3 = Operations.Add<double, double>(values1, values2);

        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), matrix1),
                new ProductTerm(new NumericFactor(1), matrix2)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Subtraction equation factory.
    /// </summary>
    /// <param name="matrix1">The matrix1.</param>
    /// <param name="matrix2">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation SubtractionEquationFactory(NumericMatrixFactor matrix1, NumericMatrixFactor matrix2, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix1.Coefficients;
        var values2 = matrix2.Coefficients;
        var values3 = Operations.Subtract<double, double>(values1, values2);

        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), matrix1),
                new ProductTerm(new NumericFactor(-1), matrix2)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Product equation factory.
    /// </summary>
    /// <param name="matrix1">The matrix1.</param>
    /// <param name="matrix2">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation ProductEquationFactory(NumericMatrixFactor matrix1, NumericMatrixFactor matrix2, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix1.Coefficients;
        var values2 = matrix2.Coefficients;
        var values3 = Operations.Multiply<double>(values1, values2);

        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), matrix1, matrix2)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Quotients the equation factory.
    /// </summary>
    /// <param name="matrix1">The matrix1.</param>
    /// <param name="matrix2">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation QuotientEquationFactory(NumericMatrixFactor matrix1, NumericMatrixFactor matrix2, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix1.Coefficients;
        var values2 = Operations.Inverse<double>(matrix2.Coefficients);
        var values3 = Operations.Multiply<double>(values1, values2);

        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), new QuotientFactor(matrix1, matrix2))
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Scales the equation factory.
    /// </summary>
    /// <param name="scalar">The scalar.</param>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation ScaleEquationFactory(NumericFactor scalar, NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = scalar.Value;
        var values2 = matrix.Coefficients;
        var values3 = Operations.Scale(values1, values2);

        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(scalar, matrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Fractions the scale equation factory.
    /// </summary>
    /// <param name="scalar">The scalar.</param>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation FractionScaleEquationFactory(FractionFactor scalar, NumericMatrixFactor matrix, out FractionNumericMatrixFactor matrixResult)
    {
        var values1 = (double)scalar.Numerator / scalar.Denominator;
        var values2 = matrix.Coefficients;
        var values3 = Operations.Scale(values1, values2);

        matrixResult = new FractionNumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(scalar, matrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    ///// <summary>
    ///// Product equation factory.
    ///// </summary>
    ///// <param name="matrix">The matrix2.</param>
    ///// <param name="matrixResult">The matrix result.</param>
    ///// <returns></returns>
    //public static RelationalOperation LogarithmEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    //{
    //    var values1 = 1;
    //    var values2 = matrix.Coefficients;
    //    var values3 = Operations.Scale(values1, values2);

    //    matrixResult = new NumericMatrixFactor(values3, false);
    //    var equation = new RelationalOperation(
    //        ComparisonOperators.Equals,
    //        new NomialExpression(
    //            new ProductTerm(new NumericFactor(1), new LogarithmFunction(matrix, new NumericFactor(2)))
    //            ),
    //            new ProductTerm(new NumericFactor(1), matrixResult)
    //        );
    //    return equation;
    //}

    ///// <summary>
    ///// Product equation factory.
    ///// </summary>
    ///// <param name="matrix">The matrix2.</param>
    ///// <param name="matrixResult">The matrix result.</param>
    ///// <returns></returns>
    //public static RelationalOperation SquareRootEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    //{
    //    var values1 = 1;
    //    var values2 = matrix.Coefficients;
    //    var values3 = Operations.Scale(values1, values2);

    //    matrixResult = new NumericMatrixFactor(values3, false);
    //    var equation = new RelationalOperation(
    //        ComparisonOperators.Equals,
    //        new NomialExpression(
    //            new ProductTerm(new NumericFactor(1), new RootFunction(new NumericFactor(2), matrix))
    //            ),
    //            new ProductTerm(new NumericFactor(1), matrixResult)
    //        );
    //    return equation;
    //}

    ///// <summary>
    ///// Product equation factory.
    ///// </summary>
    ///// <param name="matrix">The matrix2.</param>
    ///// <param name="matrixResult">The matrix result.</param>
    ///// <returns></returns>
    //public static RelationalOperation CubeRootEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    //{
    //    var values1 = 1;
    //    var values2 = matrix.Coefficients;
    //    var values3 = Operations.Scale(values1, values2);

    //    matrixResult = new NumericMatrixFactor(values3, false);
    //    var equation = new RelationalOperation(
    //        ComparisonOperators.Equals,
    //        new NomialExpression(
    //            new ProductTerm(new NumericFactor(1), new RootFunction(new NumericFactor(3), matrix))
    //            ),
    //            new ProductTerm(new NumericFactor(1), matrixResult)
    //        );
    //    return equation;
    //}

    /// <summary>
    /// Squares the equation factory.
    /// </summary>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation SquareEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.Multiply<double>(values1, values1);
        var squareMatrix = new NumericMatrixFactor(matrix, 2, null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), squareMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Cubes the equation factory.
    /// </summary>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation CubeEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.Multiply<double>(Operations.Multiply<double>(values1, values1), values1);
        var squareMatrix = new NumericMatrixFactor(matrix, 3, null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), squareMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Inverses the equation factory.
    /// </summary>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation InverseEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.Inverse<double>(values1);
        var inverseMatrix = new NumericMatrixFactor(matrix, -1, null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), inverseMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Rotates the matrix factory.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation RotateMatrixClockwiseFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.RotateMatrixClockwise<double>(values1);
        var squareMatrix = new NumericMatrixFactor(matrix, new VariableFactor('R', 1), null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), squareMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Rotates the matrix factory.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation RotateMatrixCounterClockwiseFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.RotateMatrixCounterClockwise<double>(values1);
        var squareMatrix = new NumericMatrixFactor(matrix, new VariableFactor('R', 1), null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), squareMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Transposes the matrix equation.
    /// </summary>
    /// <param name="matrix">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation TransposeMatrixEquationFactory(NumericMatrixFactor matrix, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix.Coefficients;
        var values2 = Operations.Transpose<double>(values1);
        var squareMatrix = new NumericMatrixFactor(matrix, new VariableFactor('T', 1), null);

        matrixResult = new NumericMatrixFactor(values2, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), squareMatrix)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }

    /// <summary>
    /// Choleskies the solve.
    /// </summary>
    /// <param name="matrix1">The matrix1.</param>
    /// <param name="matrix2">The matrix2.</param>
    /// <param name="matrixResult">The matrix result.</param>
    /// <returns></returns>
    public static RelationalOperation CholeskySolve(NumericMatrixFactor matrix1, NumericMatrixFactor matrix2, out NumericMatrixFactor matrixResult)
    {
        var values1 = matrix1.Coefficients;
        var cMatrix1 = new NumericMatrixFactor(matrix1, new VariableFactor('C', 1), null);
        var values2 = matrix2.Coefficients;
        var cMatrix2 = new NumericMatrixFactor(matrix2, new VariableFactor('C', 1), null);
        var solver = new CholeskyDecomposition(values1.AsSpan2D());
        var values3 = solver.Solve(values2).Items;
        matrixResult = new NumericMatrixFactor(values3, false);
        var equation = new RelationalOperation(
            ComparisonOperators.Equals,
            new NomialExpression(
                new ProductTerm(new NumericFactor(1), cMatrix1, cMatrix2)
                ),
                new ProductTerm(new NumericFactor(1), matrixResult)
            );
        return equation;
    }
}
