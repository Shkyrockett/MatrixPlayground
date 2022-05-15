using Microsoft.Toolkit.HighPerformance;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <acknowledgment>
/// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
/// </acknowledgment>
public struct QRDecomposition
{
    #region Fields
    /// <summary>
    /// Array for internal storage of decomposition.
    /// @serial internal array storage.
    /// </summary>
    private readonly double[,] QR;

    /// <summary>
    /// Array for internal storage of diagonal of R.
    /// @serial diagonal of R.
    /// </summary>
    private readonly double[] Rdiag;
    #endregion

    #region Constructors
    /// <summary>
    /// QR Decomposition, computed by Householder reflections.
    /// </summary>
    /// <param name="A">Rectangular matrix</param>
    /// <returns>
    /// Structure to access R and the Householder vectors and compute Q.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public QRDecomposition(Span2D<double> A)
    {
        // Initialize.
        QR = Operations.CopyMatrix(A);
        var m = A.Height;
        var n = A.Width;
        Rdiag = new double[n];

        // Main loop.
        for (var k = 0; k < n; k++)
        {
            // Compute 2-norm of k-th column without under/overflow.
            double nrm = 0;
            for (var i = k; i < m; i++)
            {
                nrm = Operations.Hypotenuse<double, double>(nrm, QR[i, k]);
            }

            if (nrm != 0.0)
            {
                // Form k-th Householder vector.
                if (QR[k, k] < 0)
                {
                    nrm = -nrm;
                }

                for (var i = k; i < m; i++)
                {
                    QR[i, k] /= nrm;
                }

                QR[k, k] += 1d;

                // Apply transformation to remaining columns.
                for (var j = k + 1; j < n; j++)
                {
                    var s = 0.0;
                    for (var i = k; i < m; i++)
                    {
                        s += QR[i, k] * QR[i, j];
                    }

                    s = (-s) / QR[k, k];
                    for (var i = k; i < m; i++)
                    {
                        QR[i, j] += s * QR[i, k];
                    }
                }
            }

            Rdiag[k] = -nrm;
        }
    }

    /// <summary>
    /// QR Decomposition, computed by Householder reflections.
    /// </summary>
    /// <param name="A">Rectangular matrix</param>
    /// <returns>
    /// Structure to access R and the Householder vectors and compute Q.
    /// </returns>
    public QRDecomposition(Matrix<double> A)
        : this(A.Items.AsSpan2D())
    { }
    #endregion

    #region Public Properties
    /// <summary>
    /// Is the matrix full rank?
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if [full rank]; otherwise, <see langword="false" />.
    /// </value>
    public bool IsFullRank => Operations.IsFullRank<double>(Rdiag);

    /// <summary>
    /// Return the Householder vectors
    /// </summary>
    /// <value>
    /// The h.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> H
    {
        get
        {
            var m = QR.GetLength(0);
            var n = QR.GetLength(1);
            var X = new Matrix<double>(m, n);
            var H = X.Items;
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i >= j)
                    {
                        H[i, j] = QR[i, j];
                    }
                    else
                    {
                        H[i, j] = 0.0;
                    }
                }
            }
            return X;
        }

    }

    /// <summary>
    /// Return the upper triangular factor
    /// </summary>
    /// <value>
    /// The r.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> R
    {
        get
        {
            var n = QR.GetLength(1);
            var X = new Matrix<double>(n, n);
            var R = X.Items;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i < j)
                    {
                        R[i, j] = QR[i, j];
                    }
                    else if (i == j)
                    {
                        R[i, j] = Rdiag[i];
                    }
                    else
                    {
                        R[i, j] = 0d;
                    }
                }
            }

            return X;
        }
    }

    /// <summary>
    /// Generate and return the (economy-sized) orthogonal factor
    /// </summary>
    /// <value>
    /// The q.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> Q
    {
        get
        {
            var m = QR.GetLength(0);
            var n = QR.GetLength(1);
            var X = new Matrix<double>(m, n);
            var Q = X.Items;
            for (var k = n - 1; k >= 0; k--)
            {
                for (var i = 0; i < m; i++)
                {
                    Q[i, k] = 0.0;
                }
                Q[k, k] = 1.0;
                for (var j = k; j < n; j++)
                {
                    if (QR[k, k] != 0)
                    {
                        var s = 0.0;
                        for (var i = k; i < m; i++)
                        {
                            s += QR[i, k] * Q[i, j];
                        }
                        s = (-s) / QR[k, k];
                        for (var i = k; i < m; i++)
                        {
                            Q[i, j] += s * QR[i, k];
                        }
                    }
                }
            }
            return X;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Least squares solution of A*X = B
    /// </summary>
    /// <param name="B">A Matrix with as many rows as A and any number of columns.</param>
    /// <returns>
    /// X that minimizes the two norm of Q*R*X-B.
    /// </returns>
    /// <exception cref="ArgumentException">Matrix row dimensions must agree.</exception>
    /// <exception cref="SystemException">Matrix is rank deficient.</exception>
    public Matrix<double> Solve(Matrix<double> B) => new(Operations.QRSolve(QR, Rdiag, B.Items));
    #endregion
}
