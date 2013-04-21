using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBW;

namespace LibBifurcationDiscovery
{
    class eigenCalc
    {
        private static void swap(float[][] a, int i, int j, int m, int k)
        {
            float temp = a[i][j];
            a[i][j] = a[m][k];
            a[m][k] = temp;
        }
        public static void elmhes(float[][] a, int n)
        {
            int m, j, i;
            float y, x;

            for (m = 2; m < n; m++)
            {
                x = 0f;
                i = m;
                for (j = m; j <= n; j++)
                {
                    if (Math.Abs(a[j][m - 1]) > Math.Abs(x))
                    {
                        x = a[j][m - 1];
                        i = j;
                    }
                }

                if (i != m)
                {
                    for (j = m - 1; j <= n; j++) swap(a, i, j, m, j);
                    for (j = 1; j <= n; j++) swap(a, j, i, j, m);
                }
                if (x != 0)
                {
                    for (i = m + 1; i <= n; i++)
                    {
                        if ((y = a[i][m - 1]) != 0.0f)
                        {
                            y /= x;
                            a[i][m - 1] = y;
                            for (j = m; j <= n; j++)
                                a[i][j] -= y * a[m][j];
                            for (j = 1; j <= n; j++)
                                a[j][m] += y * a[j][i];
                        }
                    }
                }
            }
        }

        private static float SIGN(float a, float b)
        {
            return ((b) >= 0.0 ? Math.Abs(a) : -Math.Abs(a));
        }

        private static float SIGN(double a, double b)
        {
            return (float)((b) >= 0.0 ? Math.Abs(a) : -Math.Abs(a));
        }

        public static void hqr(float[][] a, int n, float[] wr, float[] wi)
        {
            int nn, m, l, k, j, its, i, mmin;
            float z = 0f, y = 0f, x = 0f, w = 0f, v = 0f, u = 0f, t = 0f, s = 0f, r = 0f, q = 0f, p = 0f, anorm;
            anorm = 0.0f;
            for (i = 1; i <= n; i++)
                for (j = Math.Max(i - 1, 1); j <= n; j++)
                    anorm += Math.Abs(a[i][j]);
            nn = n;
            t = 0.0f;
            while (nn >= 1)
            {
                its = 0;
                do
                {
                    for (l = nn; l >= 2; l--)
                    {
                        s = Math.Abs(a[l - 1][l - 1]) + Math.Abs(a[l][l]);
                        if (s == 0.0f)
                            s = anorm;
                        if ((float)(Math.Abs(a[l][l - 1]) + s) == s)
                        {
                            a[l][l - 1] = 0.0f;
                            break;
                        }
                    }
                    x = a[nn][nn];
                    if (l == nn)
                    {
                        wr[nn] = x + t;
                        wi[nn--] = 0.0f;
                    }
                    else
                    {
                        y = a[nn - 1][nn - 1];
                        w = a[nn][nn - 1] * a[nn - 1][nn];
                        if (l == (nn - 1))
                        {
                            p = 0.5f * (y - x);
                            q = p * p + w;
                            z = (float)Math.Sqrt(Math.Abs(q));
                            x += t;
                            if (q >= 0.0)
                            {
                                z = p + SIGN(z, p);
                                wr[nn - 1] = wr[nn] = x + z;
                                if (z != 0f)
                                    wr[nn] = x - w / z;
                                wi[nn - 1] = wi[nn] = 0.0f;
                            }
                            else
                            {
                                wr[nn - 1] = wr[nn] = x + p;
                                wi[nn - 1] = -(wi[nn] = z);
                            }
                            nn -= 2;
                        }
                        else
                        {
                            if (its == 30)
                            {
                                throw new SBWApplicationException("Error occured in Eigen Computation", "Too many iterations in hqr");
                            }
                            if (its == 10 || its == 20)
                            {
                                t += x;
                                for (i = 1; i <= nn; i++)
                                    a[i][i] -= x;
                                s = Math.Abs(a[nn][nn - 1]) + Math.Abs(a[nn - 1][nn - 2]);
                                y = x = 0.75f * s;
                                w = -0.4375f * s * s;
                            }
                            ++its;
                            for (m = (nn - 2); m >= l; m--)
                            {
                                z = a[m][m];
                                r = x - z;
                                s = y - z;
                                p = (r * s - w) / a[m + 1][m] + a[m][m + 1];
                                q = a[m + 1][m + 1] - z - r - s;
                                r = a[m + 2][m + 1];
                                s = Math.Abs(p) + Math.Abs(q) + Math.Abs(r); p /= s;
                                q /= s;
                                r /= s;
                                if (m == l)
                                    break;
                                u = Math.Abs(a[m][m - 1]) * (Math.Abs(q) + Math.Abs(r));
                                v = Math.Abs(p) * (Math.Abs(a[m - 1][m - 1]) + Math.Abs(z) + Math.Abs(a[m + 1][m + 1]));
                                if ((float)(u + v) == v)
                                    break;
                            }
                            for (i = m + 2; i <= nn; i++)
                            {
                                a[i][i - 2] = 0.0f;
                                if (i != (m + 2))
                                    a[i][i - 3] = 0.0f;
                            }
                            for (k = m; k <= nn - 1; k++)
                            {

                                if (k != m)
                                {
                                    p = a[k][k - 1];
                                    q = a[k + 1][k - 1];
                                    r = 0.0f;
                                    if (k != (nn - 1))
                                        r = a[k + 2][k - 1];
                                    if ((x = Math.Abs(p) + Math.Abs(q) + Math.Abs(r)) != 0.0)
                                    {
                                        p /= x;
                                        q /= x;
                                        r /= x;
                                    }
                                }
                                if ((s = SIGN(Math.Sqrt(p * p + q * q + r * r), p)) != 0.0)
                                {
                                    if (k == m)
                                    {
                                        if (l != m)
                                            a[k][k - 1] = -a[k][k - 1];
                                    }
                                    else
                                        a[k][k - 1] = -s * x;
                                    p += s;
                                    x = p / s;
                                    y = q / s;
                                    z = r / s;
                                    q /= p;
                                    r /= p;
                                    for (j = k; j <= nn; j++)
                                    {
                                        p = a[k][j] + q * a[k + 1][j];
                                        if (k != (nn - 1))
                                        {
                                            p += r * a[k + 2][j];
                                            a[k + 2][j] -= p * z;
                                        }
                                        a[k + 1][j] -= p * y;
                                        a[k][j] -= p * x;
                                    }
                                    mmin = nn < k + 3 ? nn : k + 3;
                                    for (i = l; i <= mmin; i++)
                                    {
                                        p = x * a[i][k] + y * a[i][k + 1];
                                        if (k != (nn - 1))
                                        {
                                            p += z * a[i][k + 2];
                                            a[i][k + 2] -= p * r;
                                        }
                                        a[i][k + 1] -= p * q;
                                        a[i][k] -= p;
                                    }
                                }
                            }
                        }
                    }
                } while (l < nn - 1);
            }
        }
    }
}
