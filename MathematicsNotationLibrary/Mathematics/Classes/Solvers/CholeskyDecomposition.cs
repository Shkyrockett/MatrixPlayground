using Microsoft.Toolkit.HighPerformance;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <acknowledgment>
/// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
/// </acknowledgment>
public struct CholeskyDecomposition
{
    #region Fields
    /// <summary>
    /// Array for internal storage of decomposition.
    /// @serial internal array storage.
    /// </summary>
    private readonly double[,] L;

    /// <summary>
    /// Symmetric and positive definite flag.
    /// @serial is symmetric and positive definite flag.
    /// </summary>
    private readonly bool isspd;
    #endregion

    #region Constructor
    /// <summary>
    /// Cholesky algorithm for symmetric and positive definite matrix.
    /// </summary>
    /// <param name="Arg">Square, symmetric matrix.</param>
    /// <returns>
    /// Structure to access L and isspd flag.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public CholeskyDecomposition(Span2D<double> Arg)
    {
        // Initialize.
        var m = Arg.Height;
        var n = Arg.Width;
        L = new double[n, n];

        isspd = m == n;

        // Main loop.
        for (var j = 0; j < n; j++)
        {
            var Lrowj = new double[n];
            for (int i = 0; i < n; i++)
            {
                Lrowj[i] = L[j, i];
            }

            var d = 0d;
            for (var k = 0; k < j; k++)
            {
                var Lrowk = new double[n];
                for (int i = 0; i < n; i++)
                {
                    Lrowk[i] = L[k, i];
                }

                var s = 0d;
                for (var i = 0; i < k; i++)
                {
                    s += Lrowk[i] * Lrowj[i];
                }

                Lrowj[k] = s = (Arg[j, k] - s) / L[k, k];
                d += s * s;
                isspd &= Arg[k, j] == Arg[j, k];
            }

            d = Arg[j, j] - d;
            isspd &= d > 0d;
            L[j, j] = Math.Sqrt(Math.Max(d, 0d));
            for (var k = j + 1; k < n; k++)
            {
                L[j, k] = 0d;
            }
        }
    }

    /// <summary>
    /// Cholesky algorithm for symmetric and positive definite matrix.
    /// </summary>
    /// <param name="Arg">Square, symmetric matrix.</param>
    /// <returns>
    /// Structure to access L and isspd flag.
    /// </returns>
    public CholeskyDecomposition(Matrix<double> Arg)
        : this(Arg.Items.AsSpan2D())
    { }
    #endregion

    #region Public Properties
    /// <summary>
    /// Is the matrix symmetric and positive definite?
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if SPD; otherwise, <see langword="false" />.
    /// </value>
    public bool SPD => isspd;
    #endregion

    #region Public Methods
    /// <summary>
    /// Return triangular factor.
    /// </summary>
    /// <returns>
    /// L
    /// </returns>
    public Matrix<double> GetL() => new(L);

    /// <summary>
    /// Solve A*X = B
    /// </summary>
    /// <param name="B">A Matrix with as many rows as A and any number of columns.</param>
    /// <returns>
    /// X so that L*L'*X = B
    /// </returns>
    /// <exception cref="ArgumentException">Matrix row dimensions must agree.</exception>
    /// <exception cref="SystemException">Matrix is not symmetric positive definite.</exception>
    public Matrix<double> Solve(Matrix<double> B) => new(Operations.CholeskySolve<double>(L, B.Items));
    #endregion
}
