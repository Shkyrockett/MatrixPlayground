using Microsoft.Toolkit.HighPerformance;
using System;

namespace MathematicsNotationLibrary;

/// <summary>
/// 
/// </summary>
/// <acknowledgment>
/// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
/// </acknowledgment>
public struct SingularValueDecomposition
{
    #region Fields
    /// <summary>
    /// Arrays for internal storage of U and V.
    /// </summary>
    private readonly double[,] U;

    /// <summary>
    /// Arrays for internal storage of U and V.
    /// </summary>
    private readonly double[,] V;

    /// <summary>
    /// Array for internal storage of singular values.
    /// </summary>
    private readonly double[] s;
    #endregion

    #region Constructor
    /// <summary>
    /// Construct the singular value decomposition
    /// </summary>
    /// <param name="Arg">Rectangular matrix</param>
    /// <returns>
    /// Structure to access U, S and V.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public SingularValueDecomposition(Span2D<double> Arg)
    {
        // Derived from LINPACK code.
        // Initialize.
        var A = Operations.CopyMatrix(Arg);
        var m = Arg.Height;
        var n = Arg.Width;
        var nu = Math.Min(m, n);
        s = new double[Math.Min(m + 1, n)];
        U = new double[m, nu];
        V = new double[n, n];
        var e = new double[n];
        var work = new double[m];
        var wantu = true;
        var wantv = true;

        // Reduce A to bidiagonal form, storing the diagonal elements
        // in s and the super-diagonal elements in e.

        var nct = Math.Min(m - 1, n);
        var nrt = Math.Max(0, Math.Min(n - 2, m));
        for (var k = 0; k < Math.Max(nct, nrt); k++)
        {
            if (k < nct)
            {

                // Compute the transformation for the k-th column and
                // place the k-th diagonal in s[k].
                // Compute 2-norm of k-th column without under/overflow.
                s[k] = 0;
                for (var i = k; i < m; i++)
                {
                    s[k] = Operations.Hypotenuse<double, double>(s[k], A[i, k]);
                }
                if (s[k] != 0.0)
                {
                    if (A[k, k] < 0.0)
                    {
                        s[k] = -s[k];
                    }
                    for (var i = k; i < m; i++)
                    {
                        A[i, k] /= s[k];
                    }
                    A[k, k] += 1.0;
                }
                s[k] = -s[k];
            }
            for (var j = k + 1; j < n; j++)
            {
                if ((k < nct) & (s[k] != 0.0))
                {

                    // Apply the transformation.

                    double t = 0;
                    for (var i = k; i < m; i++)
                    {
                        t += A[i, k] * A[i, j];
                    }
                    t = (-t) / A[k, k];
                    for (var i = k; i < m; i++)
                    {
                        A[i, j] += t * A[i, k];
                    }
                }

                // Place the k-th row of A into e for the
                // subsequent calculation of the row transformation.

                e[j] = A[k, j];
            }
            if (wantu & (k < nct))
            {

                // Place the transformation in U for subsequent back
                // multiplication.

                for (var i = k; i < m; i++)
                {
                    U[i, k] = A[i, k];
                }
            }
            if (k < nrt)
            {

                // Compute the k-th row transformation and place the
                // k-th super-diagonal in e[k].
                // Compute 2-norm without under/overflow.
                e[k] = 0;
                for (var i = k + 1; i < n; i++)
                {
                    e[k] = Operations.Hypotenuse<double, double>(e[k], e[i]);
                }
                if (e[k] != 0.0)
                {
                    if (e[k + 1] < 0.0)
                    {
                        e[k] = -e[k];
                    }
                    for (var i = k + 1; i < n; i++)
                    {
                        e[i] /= e[k];
                    }
                    e[k + 1] += 1.0;
                }
                e[k] = -e[k];
                if ((k + 1 < m) & (e[k] != 0.0))
                {

                    // Apply the transformation.

                    for (var i = k + 1; i < m; i++)
                    {
                        work[i] = 0.0;
                    }
                    for (var j = k + 1; j < n; j++)
                    {
                        for (var i = k + 1; i < m; i++)
                        {
                            work[i] += e[j] * A[i, j];
                        }
                    }
                    for (var j = k + 1; j < n; j++)
                    {
                        var t = (-e[j]) / e[k + 1];
                        for (var i = k + 1; i < m; i++)
                        {
                            A[i, j] += t * work[i];
                        }
                    }
                }
                if (wantv)
                {

                    // Place the transformation in V for subsequent
                    // back multiplication.

                    for (var i = k + 1; i < n; i++)
                    {
                        V[i, k] = e[i];
                    }
                }
            }
        }

        // Set up the final bidiagonal matrix or order p.

        var p = Math.Min(n, m + 1);
        if (nct < n)
        {
            s[nct] = A[nct, nct];
        }
        if (m < p)
        {
            s[p - 1] = 0.0;
        }
        if (nrt + 1 < p)
        {
            e[nrt] = A[nrt, p - 1];
        }
        e[p - 1] = 0.0;

        // If required, generate U.

        if (wantu)
        {
            for (var j = nct; j < nu; j++)
            {
                for (var i = 0; i < m; i++)
                {
                    U[i, j] = 0.0;
                }
                U[j, j] = 1.0;
            }
            for (var k = nct - 1; k >= 0; k--)
            {
                if (s[k] != 0.0)
                {
                    for (var j = k + 1; j < nu; j++)
                    {
                        double t = 0;
                        for (var i = k; i < m; i++)
                        {
                            t += U[i, k] * U[i, j];
                        }
                        t = (-t) / U[k, k];
                        for (var i = k; i < m; i++)
                        {
                            U[i, j] += t * U[i, k];
                        }
                    }
                    for (var i = k; i < m; i++)
                    {
                        U[i, k] = -U[i, k];
                    }
                    U[k, k] = 1.0 + U[k, k];
                    for (var i = 0; i < k - 1; i++)
                    {
                        U[i, k] = 0.0;
                    }
                }
                else
                {
                    for (var i = 0; i < m; i++)
                    {
                        U[i, k] = 0.0;
                    }
                    U[k, k] = 1.0;
                }
            }
        }

        // If required, generate V.

        if (wantv)
        {
            for (var k = n - 1; k >= 0; k--)
            {
                if ((k < nrt) & (e[k] != 0.0))
                {
                    for (var j = k + 1; j < nu; j++)
                    {
                        double t = 0;
                        for (var i = k + 1; i < n; i++)
                        {
                            t += V[i, k] * V[i, j];
                        }
                        t = (-t) / V[k + 1, k];
                        for (var i = k + 1; i < n; i++)
                        {
                            V[i, j] += t * V[i, k];
                        }
                    }
                }
                for (var i = 0; i < n; i++)
                {
                    V[i, k] = 0.0;
                }
                V[k, k] = 1.0;
            }
        }

        // Main iteration loop for the singular values.

        var pp = p - 1;
        var iter = 0;
        var eps = Math.Pow(2.0, -52.0);
        while (p > 0)
        {
            int k, kase;

            // Here is where a test for too many iterations would go.

            // This section of the program inspects for
            // negligible elements in the s and e arrays.  On
            // completion the variables kase and k are set as follows.

            // kase = 1     if s(p) and e[k-1] are negligible and k<p
            // kase = 2     if s(k) is negligible and k<p
            // kase = 3     if e[k-1] is negligible, k<p, and
            //              s(k), ..., s(p) are not negligible (qr step).
            // kase = 4     if e(p-1) is negligible (convergence).

            for (k = p - 2; k >= -1; k--)
            {
                if (k == -1)
                {
                    break;
                }
                if (Math.Abs(e[k]) <= eps * (Math.Abs(s[k]) + Math.Abs(s[k + 1])))
                {
                    e[k] = 0.0;
                    break;
                }
            }
            if (k == p - 2)
            {
                kase = 4;
            }
            else
            {
                int ks;
                for (ks = p - 1; ks >= k; ks--)
                {
                    if (ks == k)
                    {
                        break;
                    }
                    var t = (ks != p ? Math.Abs(e[ks]) : 0.0) + (ks != k + 1 ? Math.Abs(e[ks - 1]) : 0.0);
                    if (Math.Abs(s[ks]) <= eps * t)
                    {
                        s[ks] = 0.0;
                        break;
                    }
                }
                if (ks == k)
                {
                    kase = 3;
                }
                else if (ks == p - 1)
                {
                    kase = 1;
                }
                else
                {
                    kase = 2;
                    k = ks;
                }
            }
            k++;

            // Perform the task indicated by kase.

            switch (kase)
            {


                // Deflate negligible s(p).
                case 1:
                    {
                        var f = e[p - 2];
                        e[p - 2] = 0.0;
                        for (var j = p - 2; j >= k; j--)
                        {
                            var t = Operations.Hypotenuse<double, double>(s[j], f);
                            var cs = s[j] / t;
                            var sn = f / t;
                            s[j] = t;
                            if (j != k)
                            {
                                f = (-sn) * e[j - 1];
                                e[j - 1] = cs * e[j - 1];
                            }
                            if (wantv)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    t = cs * V[i, j] + sn * V[i, p - 1];
                                    V[i, p - 1] = (-sn) * V[i, j] + cs * V[i, p - 1];
                                    V[i, j] = t;
                                }
                            }
                        }
                    }
                    break;

                // Split at negligible s(k).


                case 2:
                    {
                        var f = e[k - 1];
                        e[k - 1] = 0.0;
                        for (var j = k; j < p; j++)
                        {
                            var t = Operations.Hypotenuse<double, double>(s[j], f);
                            var cs = s[j] / t;
                            var sn = f / t;
                            s[j] = t;
                            f = (-sn) * e[j];
                            e[j] = cs * e[j];
                            if (wantu)
                            {
                                for (var i = 0; i < m; i++)
                                {
                                    t = cs * U[i, j] + sn * U[i, k - 1];
                                    U[i, k - 1] = (-sn) * U[i, j] + cs * U[i, k - 1];
                                    U[i, j] = t;
                                }
                            }
                        }
                    }
                    break;

                // Perform one qr step.


                case 3:
                    {
                        // Calculate the shift.

                        var scale = Math.Max(Math.Max(Math.Max(Math.Max(Math.Abs(s[p - 1]), Math.Abs(s[p - 2])), Math.Abs(e[p - 2])), Math.Abs(s[k])), Math.Abs(e[k]));
                        var sp = s[p - 1] / scale;
                        var spm1 = s[p - 2] / scale;
                        var epm1 = e[p - 2] / scale;
                        var sk = s[k] / scale;
                        var ek = e[k] / scale;
                        var b = ((spm1 + sp) * (spm1 - sp) + epm1 * epm1) / 2.0;
                        var c = sp * epm1 * (sp * epm1);
                        var shift = 0.0;
                        if ((b != 0.0) | (c != 0.0))
                        {
                            shift = Math.Sqrt(b * b + c);
                            if (b < 0.0)
                            {
                                shift = -shift;
                            }
                            shift = c / (b + shift);
                        }
                        var f = (sk + sp) * (sk - sp) + shift;
                        var g = sk * ek;

                        // Chase zeros.

                        for (var j = k; j < p - 1; j++)
                        {
                            var t = Operations.Hypotenuse<double, double>(f, g);
                            var cs = f / t;
                            var sn = g / t;
                            if (j != k)
                            {
                                e[j - 1] = t;
                            }
                            f = cs * s[j] + sn * e[j];
                            e[j] = cs * e[j] - sn * s[j];
                            g = sn * s[j + 1];
                            s[j + 1] = cs * s[j + 1];
                            if (wantv)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    t = cs * V[i, j] + sn * V[i, j + 1];
                                    V[i, j + 1] = (-sn) * V[i, j] + cs * V[i, j + 1];
                                    V[i, j] = t;
                                }
                            }
                            t = Operations.Hypotenuse<double, double>(f, g);
                            cs = f / t;
                            sn = g / t;
                            s[j] = t;
                            f = cs * e[j] + sn * s[j + 1];
                            s[j + 1] = (-sn) * e[j] + cs * s[j + 1];
                            g = sn * e[j + 1];
                            e[j + 1] = cs * e[j + 1];
                            if (wantu && (j < m - 1))
                            {
                                for (var i = 0; i < m; i++)
                                {
                                    t = cs * U[i, j] + sn * U[i, j + 1];
                                    U[i, j + 1] = (-sn) * U[i, j] + cs * U[i, j + 1];
                                    U[i, j] = t;
                                }
                            }
                        }
                        e[p - 2] = f;
                        iter++;
                    }
                    break;

                // Convergence.


                case 4:
                    {
                        // Make the singular values positive.

                        if (s[k] <= 0.0)
                        {
                            s[k] = s[k] < 0.0 ? -s[k] : 0.0;
                            if (wantv)
                            {
                                for (var i = 0; i <= pp; i++)
                                {
                                    V[i, k] = -V[i, k];
                                }
                            }
                        }

                        // Order the singular values.

                        while (k < pp)
                        {
                            if (s[k] >= s[k + 1])
                            {
                                break;
                            }
                            var t = s[k];
                            s[k] = s[k + 1];
                            s[k + 1] = t;
                            if (wantv && (k < n - 1))
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    t = V[i, k + 1];
                                    V[i, k + 1] = V[i, k];
                                    V[i, k] = t;
                                }
                            }
                            if (wantu && (k < m - 1))
                            {
                                for (var i = 0; i < m; i++)
                                {
                                    t = U[i, k + 1];
                                    U[i, k + 1] = U[i, k];
                                    U[i, k] = t;
                                }
                            }
                            k++;
                        }
                        iter = 0;
                        p--;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Construct the singular value decomposition
    /// </summary>
    /// <param name="Arg">Rectangular matrix</param>
    /// <returns>
    /// Structure to access U, S and V.
    /// </returns>
    public SingularValueDecomposition(Matrix<double> Arg)
        : this(Arg.Items.AsSpan2D())
    { }
    #endregion

    #region Public Properties
    /// <summary>
    /// Return the one-dimensional array of singular values
    /// </summary>
    /// <value>
    /// The singular values.
    /// </value>
    public double[] SingularValues => s;

    /// <summary>
    /// Return the diagonal matrix of singular values
    /// </summary>
    /// <value>
    /// The s.
    /// </value>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public Matrix<double> S
    {
        get
        {
            var n = s.GetLength(1);
            var X = new Matrix<double>(n, n);
            var S = X.Items;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    S[i, j] = 0.0;
                }

                S[i, i] = s[i];
            }

            return X;
        }
    }
    #endregion

    #region	 Public Methods
    /// <summary>
    /// Return the left singular vectors
    /// </summary>
    /// <returns>
    /// U
    /// </returns>
    public Matrix<double> GetU() => new(U);

    /// <summary>
    /// Return the right singular vectors
    /// </summary>
    /// <returns>
    /// V
    /// </returns>
    public Matrix<double> GetV() => new(V);

    /// <summary>
    /// Two norm
    /// </summary>
    /// <returns>
    /// max(S)
    /// </returns>
    public double Norm2() => s[0];

    /// <summary>
    /// Two norm condition number
    /// </summary>
    /// <returns>
    /// max(S)/min(S)
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public double Condition() => s[0] / s[Math.Min(s.GetLength(0), s.GetLength(1)) - 1];

    /// <summary>
    /// Effective numerical matrix rank
    /// </summary>
    /// <returns>
    /// Number of non-negligible singular values.
    /// </returns>
    /// <acknowledgment>
    /// https://www.codeproject.com/articles/5835/dotnetmatrix-simple-matrix-library-for-net
    /// </acknowledgment>
    public int Rank()
    {
        var eps = Math.Pow(2.0, -52.0);
        var tol = Math.Max(s.GetLength(0), s.GetLength(1)) * s[0] * eps;
        var r = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] > tol)
            {
                r++;
            }
        }
        return r;
    }
    #endregion
}
