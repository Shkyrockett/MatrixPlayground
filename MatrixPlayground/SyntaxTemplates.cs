namespace MatrixPlayground
{
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
        public static RelationalOperation AdditionEquationFactory(NumberMatrixFactor matrix1, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = matrix1.Coefficients;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Add(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), matrix1),
                    new ProductTerm(new CoefficientFactor(1), matrix2)
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
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
        public static RelationalOperation SubtractionEquationFactory(NumberMatrixFactor matrix1, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = matrix1.Coefficients;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Subtract(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), matrix1),
                    new ProductTerm(new CoefficientFactor(-1), matrix2)
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
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
        public static RelationalOperation ProductEquationFactory(NumberMatrixFactor matrix1, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = matrix1.Coefficients;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Multiply(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), matrix1, matrix2)
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
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
        public static RelationalOperation QuotientEquationFactory(NumberMatrixFactor matrix1, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = matrix1.Coefficients;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Multiply(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), new QuotientFactor(matrix1, matrix2))
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
                );
            return equation;
        }

        /// <summary>
        /// Product equation factory.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <param name="matrix2">The matrix2.</param>
        /// <param name="matrixResult">The matrix result.</param>
        /// <returns></returns>
        public static RelationalOperation ScaleEquationFactory(CoefficientFactor scalar, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = scalar.Value;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Scale(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(scalar, matrix2)
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
                );
            return equation;
        }

        /// <summary>
        /// Product equation factory.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <param name="matrix2">The matrix2.</param>
        /// <param name="matrixResult">The matrix result.</param>
        /// <returns></returns>
        public static RelationalOperation FractionScaleEquationFactory(FractionCoefficientFactor scalar, NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = scalar.Value;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Scale(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(scalar, matrix2)
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
                );
            return equation;
        }

        /// <summary>
        /// Product equation factory.
        /// </summary>
        /// <param name="matrix2">The matrix2.</param>
        /// <param name="matrixResult">The matrix result.</param>
        /// <returns></returns>
        public static RelationalOperation LogarithmEquationFactory(NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = 1;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Scale(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), new LogarithmFactor(matrix2, new CoefficientFactor(2)))
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
                );
            return equation;
        }

        /// <summary>
        /// Product equation factory.
        /// </summary>
        /// <param name="matrix2">The matrix2.</param>
        /// <param name="matrixResult">The matrix result.</param>
        /// <returns></returns>
        public static RelationalOperation CubeRootEquationFactory(NumberMatrixFactor matrix2, out NumberMatrixFactor matrixResult)
        {
            var values1 = 1;
            var values2 = matrix2.Coefficients;
            var values3 = Operations.Scale(values1, values2);

            matrixResult = new NumberMatrixFactor(values3, false);
            var equation = new RelationalOperation(
                ComparisonOperators.Equals,
                new NomialExpression(
                    new ProductTerm(new CoefficientFactor(1), new RootFactor(new CoefficientFactor(3), matrix2))
                    ),
                    new ProductTerm(new CoefficientFactor(1), matrixResult)
                );
            return equation;
        }
    }
}
