using Microsoft.Toolkit.HighPerformance;
using System;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <acknowledgment>
/// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
/// </acknowledgment>
public struct EigenvalueDecomposition
{
    #region Fields
    /// <summary>
    /// Arrays for internal storage of eigenvalues.
    /// </summary>
    private readonly double[] d;

    /// <summary>
    /// Arrays for internal storage of eigenvalues.
    /// </summary>
    private readonly double[] e;

    /// <summary>
    /// Array for internal storage of eigenvectors.
    /// </summary>
    private readonly double[,] V;

    /// <summary>
    /// Array for internal storage of non-symmetric Hessenberg form.
    /// </summary>
    private readonly double[,] H;

    /// <summary>
    /// Working storage for non-symmetric algorithm.
    /// </summary>
    private readonly double[] ort;

    /// <summary>
    /// Symmetry flag.
    /// </summary>
    private readonly bool issymmetric;

    /// <summary>
    /// Complex scalar division.
    /// </summary>
    [NonSerialized()]
    private double cdivr;

    /// <summary>
    /// The cdivi
    /// </summary>
    [NonSerialized()]
    private double cdivi;
    #endregion

    #region Constructor
    /// <summary>
    /// Check for symmetry, then construct the eigenvalue decomposition
    /// </summary>
    /// <param name="A">Square matrix.</param>
    /// <returns>
    /// Structure to access D and V.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public EigenvalueDecomposition(Span2D<double> A)
    {
        var n = A.Width;
        V = new double[n, n];
        H = new double[n, n];
        d = new double[n];
        e = new double[n];
        ort = new double[n];
        issymmetric = true;
        cdivr = 0;
        cdivi = 0;

        for (var j = 0; (j < n) & issymmetric; j++)
        {
            for (var i = 0; (i < n) & issymmetric; i++)
            {
                issymmetric = A[i, j] == A[j, i];
            }
        }

        if (issymmetric)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    V[i, j] = A[i, j];
                }
            }

            // Tridiagonalize.
            Tred2();

            // Diagonalize.
            Tql2();
        }
        else
        {
            H = new double[n, n];
            ort = new double[n];

            for (var j = 0; j < n; j++)
            {
                for (var i = 0; i < n; i++)
                {
                    H[i, j] = A[i, j];
                }
            }

            // Reduce to Hessenberg form.
            Orthes();

            // Reduce Hessenberg to real Schur form.
            Hqr2();
        }
    }

    /// <summary>
    /// Check for symmetry, then construct the eigenvalue decomposition
    /// </summary>
    /// <param name="Arg">Square matrix</param>
    /// <returns>
    /// Structure to access D and V.
    /// </returns>
    public EigenvalueDecomposition(Matrix<double> Arg)
        : this(Arg.Items.AsSpan2D())
    { }
    #endregion

    #region Public Properties
    /// <summary>
    /// Return the real parts of the eigenvalues
    /// </summary>
    /// <value>
    /// The real eigenvalues.
    /// </value>
    public double[] RealEigenvalues => d;

    /// <summary>
    /// Return the imaginary parts of the eigenvalues
    /// </summary>
    /// <value>
    /// The imag eigenvalues.
    /// </value>
    public double[] ImagEigenvalues => e;

    /// <summary>
    /// Return the block diagonal eigenvalue matrix
    /// </summary>
    /// <value>
    /// The d.
    /// </value>
    public Matrix<double> D
    {
        get
        {
            var n = d.GetLength(1);
            var X = new Matrix<double>(n, n);
            var D = X.Items;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    D[i, j] = 0.0;
                }
                D[i, i] = d[i];
                if (e[i] > 0)
                {
                    D[i, i + 1] = e[i];
                }
                else if (e[i] < 0)
                {
                    D[i, i - 1] = e[i];
                }
            }
            return X;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Symmetric Householder reduction to tridiagonal form.
    /// </summary>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    private void Tred2()
    {
        var n = d.GetLength(1);
        //  This is derived from the Algol procedures tred2 by
        //  Bowdler, Martin, Reinsch, and Wilkinson, Handbook for
        //  Auto. Comp., Vol.ii-Linear Algebra, and the corresponding
        //  Fortran subroutine in EISPACK.

        for (var j = 0; j < n; j++)
        {
            d[j] = V[n - 1, j];
        }

        // Householder reduction to tridiagonal form.
        for (var i = n - 1; i > 0; i--)
        {
            // Scale to avoid under/overflow.
            var scale = 0.0;
            var h = 0.0;
            for (var k = 0; k < i; k++)
            {
                scale += Math.Abs(d[k]);
            }
            if (scale == 0.0)
            {
                e[i] = d[i - 1];
                for (var j = 0; j < i; j++)
                {
                    d[j] = V[i - 1, j];
                    V[i, j] = 0d;
                    V[j, i] = 0d;
                }
            }
            else
            {
                // Generate Householder vector.
                for (var k = 0; k < i; k++)
                {
                    d[k] /= scale;
                    h += d[k] * d[k];
                }
                var f = d[i - 1];
                var g = Math.Sqrt(h);
                if (f > 0)
                {
                    g = -g;
                }
                e[i] = scale * g;
                h -= f * g;
                d[i - 1] = f - g;
                for (var j = 0; j < i; j++)
                {
                    e[j] = 0.0;
                }

                // Apply similarity transformation to remaining columns.
                for (var j = 0; j < i; j++)
                {
                    f = d[j];
                    V[j, i] = f;
                    g = e[j] + V[j, j] * f;
                    for (var k = j + 1; k <= i - 1; k++)
                    {
                        g += V[k, j] * d[k];
                        e[k] += V[k, j] * f;
                    }
                    e[j] = g;
                }
                f = 0.0;
                for (var j = 0; j < i; j++)
                {
                    e[j] /= h;
                    f += e[j] * d[j];
                }
                var hh = f / (h + h);
                for (var j = 0; j < i; j++)
                {
                    e[j] -= hh * d[j];
                }
                for (var j = 0; j < i; j++)
                {
                    f = d[j];
                    g = e[j];
                    for (var k = j; k <= i - 1; k++)
                    {
                        V[k, j] -= f * e[k] + g * d[k];
                    }
                    d[j] = V[i - 1, j];
                    V[i, j] = 0.0;
                }
            }
            d[i] = h;
        }

        // Accumulate transformations.
        for (var i = 0; i < n - 1; i++)
        {
            V[n - 1, i] = V[i, i];
            V[i, i] = 1.0;
            var h = d[i + 1];
            if (h != 0.0)
            {
                for (var k = 0; k <= i; k++)
                {
                    d[k] = V[k, i + 1] / h;
                }
                for (var j = 0; j <= i; j++)
                {
                    var g = 0.0;
                    for (var k = 0; k <= i; k++)
                    {
                        g += V[k, i + 1] * V[k, j];
                    }
                    for (var k = 0; k <= i; k++)
                    {
                        V[k, j] -= g * d[k];
                    }
                }
            }
            for (var k = 0; k <= i; k++)
            {
                V[k, i + 1] = 0.0;
            }
        }
        for (var j = 0; j < n; j++)
        {
            d[j] = V[n - 1, j];
            V[n - 1, j] = 0.0;
        }
        V[n - 1, n - 1] = 1.0;
        e[0] = 0.0;
    }

    /// <summary>
    /// Symmetric tridiagonal QL algorithm.
    /// </summary>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    private void Tql2()
    {
        var n = d.GetLength(1);
        //  This is derived from the Algol procedures tql2, by
        //  Bowdler, Martin, Reinsch, and Wilkinson, Handbook for
        //  Auto. Comp., Vol.ii-Linear Algebra, and the corresponding
        //  Fortran subroutine in EISPACK.

        for (var i = 1; i < n; i++)
        {
            e[i - 1] = e[i];
        }
        e[n - 1] = 0.0;

        var f = 0.0;
        var tst1 = 0.0;
        var eps = Math.Pow(2.0, -52.0);
        for (var l = 0; l < n; l++)
        {
            // Find small subdiagonal element

            tst1 = Math.Max(tst1, Math.Abs(d[l]) + Math.Abs(e[l]));
            var m = l;
            while (m < n)
            {
                if (Math.Abs(e[m]) <= eps * tst1)
                {
                    break;
                }
                m++;
            }

            // If m == l, d[l] is an eigenvalue,
            // otherwise, iterate.

            if (m > l)
            {
                var iter = 0;
                do
                {
                    iter++; // (Could check iteration count here.)

                    // Compute implicit shift

                    var g = d[l];
                    var p = (d[l + 1] - g) / (2.0 * e[l]);
                    var r = Operations.Hypotenuse<double, double>(p, 1.0);
                    if (p < 0)
                    {
                        r = -r;
                    }
                    d[l] = e[l] / (p + r);
                    d[l + 1] = e[l] * (p + r);
                    var dl1 = d[l + 1];
                    var h = g - d[l];
                    for (var i = l + 2; i < n; i++)
                    {
                        d[i] -= h;
                    }
                    f += h;

                    // Implicit QL transformation.

                    p = d[m];
                    var c = 1.0;
                    var c2 = c;
                    var c3 = c;
                    var el1 = e[l + 1];
                    var s = 0.0;
                    var s2 = 0.0;
                    for (var i = m - 1; i >= l; i--)
                    {
                        c3 = c2;
                        c2 = c;
                        s2 = s;
                        g = c * e[i];
                        h = c * p;
                        r = Operations.Hypotenuse<double, double>(p, e[i]);
                        e[i + 1] = s * r;
                        s = e[i] / r;
                        c = p / r;
                        p = c * d[i] - s * g;
                        d[i + 1] = h + s * (c * g + s * d[i]);

                        // Accumulate transformation.

                        for (var k = 0; k < n; k++)
                        {
                            h = V[k, i + 1];
                            V[k, i + 1] = s * V[k, i] + c * h;
                            V[k, i] = c * V[k, i] - s * h;
                        }
                    }
                    p = (-s) * s2 * c3 * el1 * e[l] / dl1;
                    e[l] = s * p;
                    d[l] = c * p;

                    // Check for convergence.
                }
                while (Math.Abs(e[l]) > eps * tst1);
            }
            d[l] = d[l] + f;
            e[l] = 0.0;
        }

        // Sort eigenvalues and corresponding vectors.

        for (var i = 0; i < n - 1; i++)
        {
            var k = i;
            var p = d[i];
            for (var j = i + 1; j < n; j++)
            {
                if (d[j] < p)
                {
                    k = j;
                    p = d[j];
                }
            }
            if (k != i)
            {
                d[k] = d[i];
                d[i] = p;
                for (var j = 0; j < n; j++)
                {
                    p = V[j, i];
                    V[j, i] = V[j, k];
                    V[j, k] = p;
                }
            }
        }
    }

    /// <summary>
    /// Non-symmetric reduction to Hessenberg form.
    /// </summary>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    private void Orthes()
    {
        var n = d.GetLength(1);
        //  This is derived from the Algol procedures orthes and ortran,
        //  by Martin and Wilkinson, Handbook for Auto. Comp.,
        //  Vol.ii-Linear Algebra, and the corresponding
        //  Fortran subroutines in EISPACK.

        var low = 0;
        var high = n - 1;

        for (var m = low + 1; m <= high - 1; m++)
        {

            // Scale column.

            var scale = 0d;
            for (var i = m; i <= high; i++)
            {
                scale += Math.Abs(H[i, m - 1]);
            }
            if (scale != 0d)
            {

                // Compute Householder transformation.

                var h = 0d;
                for (var i = high; i >= m; i--)
                {
                    ort[i] = H[i, m - 1] / scale;
                    h += ort[i] * ort[i];
                }
                var g = Math.Sqrt(h);
                if (ort[m] > 0)
                {
                    g = -g;
                }
                h -= ort[m] * g;
                ort[m] = ort[m] - g;

                // Apply Householder similarity transformation
                // H = (I-u*u'/h)*H*(I-u*u')/h)

                for (var j = m; j < n; j++)
                {
                    var f = 0d;
                    for (var i = high; i >= m; i--)
                    {
                        f += ort[i] * H[i, j];
                    }
                    f /= h;
                    for (var i = m; i <= high; i++)
                    {
                        H[i, j] -= f * ort[i];
                    }
                }

                for (var i = 0; i <= high; i++)
                {
                    var f = 0d;
                    for (var j = high; j >= m; j--)
                    {
                        f += ort[j] * H[i, j];
                    }
                    f /= h;
                    for (var j = m; j <= high; j++)
                    {
                        H[i, j] -= f * ort[j];
                    }
                }
                ort[m] = scale * ort[m];
                H[m, m - 1] = scale * g;
            }
        }

        // Accumulate transformations (Algol's ortran).

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                V[i, j] = i == j ? 1d : 0d;
            }
        }

        for (var m = high - 1; m >= low + 1; m--)
        {
            if (H[m, m - 1] != 0d)
            {
                for (var i = m + 1; i <= high; i++)
                {
                    ort[i] = H[i, m - 1];
                }
                for (var j = m; j <= high; j++)
                {
                    var g = 0d;
                    for (var i = m; i <= high; i++)
                    {
                        g += ort[i] * V[i, j];
                    }
                    // Double division avoids possible underflow
                    g = g / ort[m] / H[m, m - 1];
                    for (var i = m; i <= high; i++)
                    {
                        V[i, j] += g * ort[i];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Cdivs the specified xr.
    /// </summary>
    /// <param name="xr">The xr.</param>
    /// <param name="xi">The xi.</param>
    /// <param name="yr">The yr.</param>
    /// <param name="yi">The yi.</param>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    private void Cdiv(double xr, double xi, double yr, double yi)
    {
        double r, d;
        if (Math.Abs(yr) > Math.Abs(yi))
        {
            r = yi / yr;
            d = yr + r * yi;
            cdivr = (xr + r * xi) / d;
            cdivi = (xi - r * xr) / d;
        }
        else
        {
            r = yr / yi;
            d = yi + r * yr;
            cdivr = (r * xr + xi) / d;
            cdivi = (r * xi - xr) / d;
        }
    }

    /// <summary>
    /// Non-symmetric reduction from Hessenberg to real Schur form.
    /// </summary>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    private void Hqr2()
    {
        //  This is derived from the Algol procedure hqr2,
        //  by Martin and Wilkinson, Handbook for Auto. Comp.,
        //  Vol.ii-Linear Algebra, and the corresponding
        //  Fortran subroutine in EISPACK.

        // Initialize

        var nn = d.GetLength(1);
        var n = nn - 1;
        var low = 0;
        var high = nn - 1;
        var eps = Math.Pow(2.0, -52.0);
        var exshift = 0.0;
        double p = 0, q = 0, r = 0, s = 0, z = 0, t, w, x, y;

        // Store roots isolated by balanc and compute matrix norm

        var norm = 0.0;
        for (var i = 0; i < nn; i++)
        {
            if (i < low | i > high)
            {
                d[i] = H[i, i];
                e[i] = 0.0;
            }
            for (var j = Math.Max(i - 1, 0); j < nn; j++)
            {
                norm += Math.Abs(H[i, j]);
            }
        }

        // Outer loop over eigenvalue index

        var iter = 0;
        while (n >= low)
        {

            // Look for single small sub-diagonal element

            var l = n;
            while (l > low)
            {
                s = Math.Abs(H[l - 1, l - 1]) + Math.Abs(H[l, l]);
                if (s == 0.0)
                {
                    s = norm;
                }
                if (Math.Abs(H[l, l - 1]) < eps * s)
                {
                    break;
                }
                l--;
            }

            // Check for convergence
            // One root found

            if (l == n)
            {
                H[n, n] = H[n, n] + exshift;
                d[n] = H[n, n];
                e[n] = 0.0;
                n--;
                iter = 0;

                // Two roots found
            }
            else if (l == n - 1)
            {
                w = H[n, n - 1] * H[n - 1, n];
                p = (H[n - 1, n - 1] - H[n, n]) / 2d;
                q = p * p + w;
                z = Math.Sqrt(Math.Abs(q));
                H[n, n] = H[n, n] + exshift;
                H[n - 1, n - 1] = H[n - 1, n - 1] + exshift;
                x = H[n, n];

                // Real pair

                if (q >= 0)
                {
                    if (p >= 0)
                    {
                        z = p + z;
                    }
                    else
                    {
                        z = p - z;
                    }
                    d[n - 1] = x + z;
                    d[n] = d[n - 1];
                    if (z != 0.0)
                    {
                        d[n] = x - w / z;
                    }
                    e[n - 1] = 0.0;
                    e[n] = 0.0;
                    x = H[n, n - 1];
                    s = Math.Abs(x) + Math.Abs(z);
                    p = x / s;
                    q = z / s;
                    r = Math.Sqrt(p * p + q * q);
                    p /= r;
                    q /= r;

                    // Row modification

                    for (var j = n - 1; j < nn; j++)
                    {
                        z = H[n - 1, j];
                        H[n - 1, j] = q * z + p * H[n, j];
                        H[n, j] = q * H[n, j] - p * z;
                    }

                    // Column modification

                    for (var i = 0; i <= n; i++)
                    {
                        z = H[i, n - 1];
                        H[i, n - 1] = q * z + p * H[i, n];
                        H[i, n] = q * H[i, n] - p * z;
                    }

                    // Accumulate transformations

                    for (var i = low; i <= high; i++)
                    {
                        z = V[i, n - 1];
                        V[i, n - 1] = q * z + p * V[i, n];
                        V[i, n] = q * V[i, n] - p * z;
                    }

                    // Complex pair
                }
                else
                {
                    d[n - 1] = x + p;
                    d[n] = x + p;
                    e[n - 1] = z;
                    e[n] = -z;
                }
                n -= 2;
                iter = 0;

                // No convergence yet
            }
            else
            {

                // Form shift

                x = H[n, n];
                y = 0.0;
                w = 0.0;
                if (l < n)
                {
                    y = H[n - 1, n - 1];
                    w = H[n, n - 1] * H[n - 1, n];
                }

                // Wilkinson's original ad hoc shift

                if (iter == 10)
                {
                    exshift += x;
                    for (var i = low; i <= n; i++)
                    {
                        H[i, i] -= x;
                    }
                    s = Math.Abs(H[n, n - 1]) + Math.Abs(H[n - 1, n - 2]);
                    x = y = 0.75 * s;
                    w = (-0.4375) * s * s;
                }

                // MATLAB's new ad hoc shift

                if (iter == 30)
                {
                    s = (y - x) / 2.0;
                    s = s * s + w;
                    if (s > 0)
                    {
                        s = Math.Sqrt(s);
                        if (y < x)
                        {
                            s = -s;
                        }
                        s = x - w / ((y - x) / 2.0 + s);
                        for (var i = low; i <= n; i++)
                        {
                            H[i, i] -= s;
                        }
                        exshift += s;
                        x = y = w = 0.964;
                    }
                }

                iter++; // (Could check iteration count here.)

                // Look for two consecutive small sub-diagonal elements

                var m = n - 2;
                while (m >= l)
                {
                    z = H[m, m];
                    r = x - z;
                    s = y - z;
                    p = (r * s - w) / H[m + 1, m] + H[m, m + 1];
                    q = H[m + 1, m + 1] - z - r - s;
                    r = H[m + 2, m + 1];
                    s = Math.Abs(p) + Math.Abs(q) + Math.Abs(r);
                    p /= s;
                    q /= s;
                    r /= s;
                    if (m == l)
                    {
                        break;
                    }
                    if (Math.Abs(H[m, m - 1]) * (Math.Abs(q) + Math.Abs(r)) < eps * (Math.Abs(p) * (Math.Abs(H[m - 1, m - 1]) + Math.Abs(z) + Math.Abs(H[m + 1, m + 1]))))
                    {
                        break;
                    }
                    m--;
                }

                for (var i = m + 2; i <= n; i++)
                {
                    H[i, i - 2] = 0.0;
                    if (i > m + 2)
                    {
                        H[i, i - 3] = 0.0;
                    }
                }

                // Double QR step involving rows l:n and columns m:n

                for (var k = m; k <= n - 1; k++)
                {
                    var notlast = k != n - 1;
                    if (k != m)
                    {
                        p = H[k, k - 1];
                        q = H[k + 1, k - 1];
                        r = notlast ? H[k + 2, k - 1] : 0.0;
                        x = Math.Abs(p) + Math.Abs(q) + Math.Abs(r);
                        if (x != 0.0)
                        {
                            p /= x;
                            q /= x;
                            r /= x;
                        }
                    }
                    if (x == 0.0)
                    {
                        break;
                    }
                    s = Math.Sqrt(p * p + q * q + r * r);
                    if (p < 0)
                    {
                        s = -s;
                    }
                    if (s != 0)
                    {
                        if (k != m)
                        {
                            H[k, k - 1] = (-s) * x;
                        }
                        else if (l != m)
                        {
                            H[k, k - 1] = -H[k, k - 1];
                        }
                        p += s;
                        x = p / s;
                        y = q / s;
                        z = r / s;
                        q /= p;
                        r /= p;

                        // Row modification

                        for (var j = k; j < nn; j++)
                        {
                            p = H[k, j] + q * H[k + 1, j];
                            if (notlast)
                            {
                                p += r * H[k + 2, j];
                                H[k + 2, j] = H[k + 2, j] - p * z;
                            }
                            H[k, j] = H[k, j] - p * x;
                            H[k + 1, j] = H[k + 1, j] - p * y;
                        }

                        // Column modification

                        for (var i = 0; i <= Math.Min(n, k + 3); i++)
                        {
                            p = x * H[i, k] + y * H[i, k + 1];
                            if (notlast)
                            {
                                p += z * H[i, k + 2];
                                H[i, k + 2] = H[i, k + 2] - p * r;
                            }
                            H[i, k] = H[i, k] - p;
                            H[i, k + 1] = H[i, k + 1] - p * q;
                        }

                        // Accumulate transformations

                        for (var i = low; i <= high; i++)
                        {
                            p = x * V[i, k] + y * V[i, k + 1];
                            if (notlast)
                            {
                                p += z * V[i, k + 2];
                                V[i, k + 2] = V[i, k + 2] - p * r;
                            }
                            V[i, k] = V[i, k] - p;
                            V[i, k + 1] = V[i, k + 1] - p * q;
                        }
                    } // (s != 0)
                } // k loop
            } // check convergence
        } // while (n >= low)

        // Backsubstitute to find vectors of upper triangular form

        if (norm == 0.0)
        {
            return;
        }

        for (n = nn - 1; n >= 0; n--)
        {
            p = d[n];
            q = e[n];

            // Real vector

            if (q == 0)
            {
                var l = n;
                H[n, n] = 1.0;
                for (var i = n - 1; i >= 0; i--)
                {
                    w = H[i, i] - p;
                    r = 0.0;
                    for (var j = l; j <= n; j++)
                    {
                        r += H[i, j] * H[j, n];
                    }
                    if (e[i] < 0.0)
                    {
                        z = w;
                        s = r;
                    }
                    else
                    {
                        l = i;
                        if (e[i] == 0.0)
                        {
                            if (w != 0.0)
                            {
                                H[i, n] = (-r) / w;
                            }
                            else
                            {
                                H[i, n] = (-r) / (eps * norm);
                            }

                            // Solve real equations
                        }
                        else
                        {
                            x = H[i, i + 1];
                            y = H[i + 1, i];
                            q = (d[i] - p) * (d[i] - p) + e[i] * e[i];
                            t = (x * s - z * r) / q;
                            H[i, n] = t;
                            if (Math.Abs(x) > Math.Abs(z))
                            {
                                H[i + 1, n] = (-r - w * t) / x;
                            }
                            else
                            {
                                H[i + 1, n] = (-s - y * t) / z;
                            }
                        }

                        // Overflow control

                        t = Math.Abs(H[i, n]);
                        if (eps * t * t > 1)
                        {
                            for (var j = i; j <= n; j++)
                            {
                                H[j, n] = H[j, n] / t;
                            }
                        }
                    }
                }

                // Complex vector
            }
            else if (q < 0)
            {
                var l = n - 1;

                // Last vector component imaginary so matrix is triangular

                if (Math.Abs(H[n, n - 1]) > Math.Abs(H[n - 1, n]))
                {
                    H[n - 1, n - 1] = q / H[n, n - 1];
                    H[n - 1, n] = (-(H[n, n] - p)) / H[n, n - 1];
                }
                else
                {
                    Cdiv(0.0, -H[n - 1, n], H[n - 1, n - 1] - p, q);
                    H[n - 1, n - 1] = cdivr;
                    H[n - 1, n] = cdivi;
                }
                H[n, n - 1] = 0.0;
                H[n, n] = 1.0;
                for (var i = n - 2; i >= 0; i--)
                {
                    double ra, sa, vr, vi;
                    ra = 0.0;
                    sa = 0.0;
                    for (var j = l; j <= n; j++)
                    {
                        ra += H[i, j] * H[j, n - 1];
                        sa += H[i, j] * H[j, n];
                    }
                    w = H[i, i] - p;

                    if (e[i] < 0.0)
                    {
                        z = w;
                        r = ra;
                        s = sa;
                    }
                    else
                    {
                        l = i;
                        if (e[i] == 0)
                        {
                            Cdiv(-ra, -sa, w, q);
                            H[i, n - 1] = cdivr;
                            H[i, n] = cdivi;
                        }
                        else
                        {

                            // Solve complex equations

                            x = H[i, i + 1];
                            y = H[i + 1, i];
                            vr = (d[i] - p) * (d[i] - p) + e[i] * e[i] - q * q;
                            vi = (d[i] - p) * 2.0 * q;
                            if (vr == 0.0 & vi == 0.0)
                            {
                                vr = eps * norm * (Math.Abs(w) + Math.Abs(q) + Math.Abs(x) + Math.Abs(y) + Math.Abs(z));
                            }
                            Cdiv(x * r - z * ra + q * sa, x * s - z * sa - q * ra, vr, vi);
                            H[i, n - 1] = cdivr;
                            H[i, n] = cdivi;
                            if (Math.Abs(x) > (Math.Abs(z) + Math.Abs(q)))
                            {
                                H[i + 1, n - 1] = (-ra - w * H[i, n - 1] + q * H[i, n]) / x;
                                H[i + 1, n] = (-sa - w * H[i, n] - q * H[i, n - 1]) / x;
                            }
                            else
                            {
                                Cdiv(-r - y * H[i, n - 1], -s - y * H[i, n], z, q);
                                H[i + 1, n - 1] = cdivr;
                                H[i + 1, n] = cdivi;
                            }
                        }

                        // Overflow control

                        t = Math.Max(Math.Abs(H[i, n - 1]), Math.Abs(H[i, n]));
                        if (eps * t * t > 1)
                        {
                            for (var j = i; j <= n; j++)
                            {
                                H[j, n - 1] = H[j, n - 1] / t;
                                H[j, n] = H[j, n] / t;
                            }
                        }
                    }
                }
            }
        }

        // Vectors of isolated roots

        for (var i = 0; i < nn; i++)
        {
            if (i < low | i > high)
            {
                for (var j = i; j < nn; j++)
                {
                    V[i, j] = H[i, j];
                }
            }
        }

        // Back transformation to get eigenvectors of original matrix

        for (var j = nn - 1; j >= low; j--)
        {
            for (var i = low; i <= high; i++)
            {
                z = 0.0;
                for (var k = low; k <= Math.Min(j, high); k++)
                {
                    z += V[i, k] * H[k, j];
                }
                V[i, j] = z;
            }
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Return the eigenvector matrix
    /// </summary>
    /// <returns>
    /// V
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> GetV() => new(V);
    #endregion\
}
