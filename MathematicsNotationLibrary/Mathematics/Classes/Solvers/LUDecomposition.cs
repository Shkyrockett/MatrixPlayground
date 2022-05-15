using Microsoft.Toolkit.HighPerformance;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <acknowledgment>
/// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
/// </acknowledgment>
public struct LUDecomposition
{
    #region Fields
    /// <summary>Array for internal storage of decomposition.
    /// @serial internal array storage.
    /// </summary>
    private readonly double[,] LU;

    /// <summary>Row and column dimensions, and pivot sign.
    /// @serial pivot sign.
    /// </summary>
    private readonly int pivotSign;

    /// <summary>Internal storage of pivot vector.
    /// @serial pivot vector.
    /// </summary>
    private readonly int[] piv;
    #endregion

    #region Constructors
    /// <summary>
    /// LU Decomposition
    /// </summary>
    /// <param name="matrix">Rectangular matrix</param>
    /// <returns>
    /// Structure to access L, U and piv.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public LUDecomposition(Span2D<double> matrix)
    {
        // Use a "left-looking", dot-product, Crout/Doolittle algorithm.
        LU = Operations.CopyMatrix(matrix);
        var m = matrix.Height;
        var n = matrix.Width;
        piv = new int[m];
        for (var i = 0; i < m; i++)
        {
            piv[i] = i;
        }

        pivotSign = 1;
        double[] LUrowi = new double[n];
        var LUcolj = new double[m];

        // Outer loop.
        for (var j = 0; j < n; j++)
        {
            // Make a copy of the j-th column to localize references.
            for (var i = 0; i < m; i++)
            {
                LUcolj[i] = LU[i, j];
            }

            // Apply previous transformations.
            for (var i = 0; i < m; i++)
            {
                for (var k = 0; k < n; k++)
                {
                    LUrowi[k] = LU[i, k];
                }

                // Most of the time is spent in the following dot product.
                var kmax = Math.Min(i, j);
                var s = 0.0;
                for (var k = 0; k < kmax; k++)
                {
                    s += LUrowi[k] * LUcolj[k];
                }

                LUrowi[j] = LUcolj[i] -= s;
            }

            // Find pivot and exchange if necessary.

            var p = j;
            for (var i = j + 1; i < m; i++)
            {
                if (Math.Abs(LUcolj[i]) > Math.Abs(LUcolj[p]))
                {
                    p = i;
                }
            }
            if (p != j)
            {
                for (var k = 0; k < n; k++)
                {
                    var t = LU[p, k]; LU[p, k] = LU[j, k]; LU[j, k] = t;
                }
                var k2 = piv[p]; piv[p] = piv[j]; piv[j] = k2;
                pivotSign = -pivotSign;
            }

            // Compute multipliers.
            if (j < m & LU[j, j] != 0.0)
            {
                for (var i = j + 1; i < m; i++)
                {
                    LU[i, j] /= LU[j, j];
                }
            }
        }
    }

    /// <summary>
    /// LU Decomposition
    /// </summary>
    /// <param name="A">Rectangular matrix</param>
    /// <returns>
    /// Structure to access L, U and piv.
    /// </returns>
    public LUDecomposition(Matrix<double> A)
        : this(A.Items.AsSpan2D())
    { }
    #endregion

    #region Properties
    /// <summary>
    /// Is the matrix nonsingular?
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if this instance is non singular; otherwise, <see langword="false" />.
    /// </value>
    public bool IsNonSingular => Operations.IsNonSingular<double>(LU);

    /// <summary>
    /// Return lower triangular factor
    /// </summary>
    /// <value>
    /// The l.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> L
    {
        get
        {
            var m = LU.GetLength(0);
            var n = LU.GetLength(1);
            var X = new Matrix<double>(m, n);
            var L = X.Items;
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i > j)
                    {
                        L[i, j] = LU[i, j];
                    }
                    else if (i == j)
                    {
                        L[i, j] = 1d;
                    }
                    else
                    {
                        L[i, j] = 0d;
                    }
                }
            }

            return X;
        }
    }

    /// <summary>
    /// Return upper triangular factor
    /// </summary>
    /// <value>
    /// The u.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> U
    {
        get
        {
            var n = LU.GetLength(1);
            var X = new Matrix<double>(n, n);
            var U = X.Items;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i <= j)
                    {
                        U[i, j] = LU[i, j];
                    }
                    else
                    {
                        U[i, j] = 0d;
                    }
                }
            }
            return X;
        }
    }

    /// <summary>
    /// Return pivot permutation vector
    /// </summary>
    /// <value>
    /// The pivot.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public int[] Pivot
    {
        get
        {
            var m = LU.GetLength(0);
            var p = new int[m];
            for (var i = 0; i < m; i++)
            {
                p[i] = piv[i];
            }

            return p;
        }
    }

    /// <summary>
    /// Return pivot permutation vector as a one-dimensional double array
    /// </summary>
    /// <value>
    /// The double pivot.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public double[] DoublePivot
    {
        get
        {
            var m = LU.GetLength(0);
            var vals = new double[m];
            for (var i = 0; i < m; i++)
            {
                vals[i] = piv[i];
            }

            return vals;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Determinant
    /// </summary>
    /// <returns>
    /// det(A)
    /// </returns>
    /// <exception cref="ArgumentException">Matrix must be square</exception>
    public double Determinant() => Operations.Determinant<double>(LU);

    /// <summary>
    /// Solve A*X = B
    /// </summary>
    /// <param name="B">A Matrix with as many rows as A and any number of columns.</param>
    /// <returns>
    /// X so that L*U*X = B(piv,:)
    /// </returns>
    /// <exception cref="ArgumentException">Matrix row dimensions must agree.</exception>
    /// <exception cref="SystemException">Matrix is singular.</exception>
    public Matrix<double> Solve(Matrix<double> B) => new(Operations.LUSolve<double>(LU, piv, B.Items));
    #endregion
}
