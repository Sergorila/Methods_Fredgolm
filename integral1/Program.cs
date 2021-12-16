using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integral1
{
    class Program
    {
        static double T = 8.0;
        static int n = 3;

        public static double[] SimpleIterat(double[,] matrix, int e)
        {
            int n = matrix.GetLength(0);
            double[,] tempMatrix = new double[n, n + 1];
            double eps = double.MaxValue;
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        tempMatrix[i, j] = -matrix[i, j] / matrix[i, i];
                    }
                }
                tempMatrix[i, n] = matrix[i, n-1] / matrix[i, i];
                result[i] = tempMatrix[i, n-1];
            }
            while (Math.Round(eps, e) != 0)
            {
                for (int i = 0; i < n; i++)
                {
                    double temp = 0;
                    for (int j = 0; j < n; j++)
                    {
                        temp += tempMatrix[i, j] * result[j];
                    }
                    temp += tempMatrix[i, n];
                    if (Math.Abs(temp - result[i]) < eps)
                    {
                        eps = Math.Abs(temp - result[i]);
                    }
                    result[i] = temp;
                }
            }
            return result;
        }
        public static double f(double x)
        {
            return T * (4 * x / 3 + 1 * x * x / 4 + 1 * x * x * x / 5);
        }
        public static double yt(double x)
        {
            return x * T;
        }
        public static double ai(double x, int i)
        {
            return Math.Pow(x, i);
        }
        public static double bk(double x, int i)
        {
            return Math.Pow(x, i);
        }
        static void Main(string[] args)
        {
            double h = 0.1;
            List<double> x = new List<double>();
            List<double> ytoch = new List<double>();
            double[][] alpha = new double[10][];
            alpha[0] = new double[] { 1 / 3, 1 / 4, 1 / 5 };
            alpha[1] = new double[] { 1 / 4, 1 / 5, 1 / 6 };
            alpha[2] = new double[] { 1 / 5, 1 / 6, 1 / 7 };
            double[] g = new double[] { T * 1969 / 3600, T * 5 / 12, T * 283 / 840 };
            List<double> fx = new List<double>();

            double cur = 0;
            while (cur < 1)
            {
                x.Add(cur);
                ytoch.Add(yt(cur));
                fx.Add(f(cur));
                cur += h;
            }

            double[,] matrix = new double[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    matrix[i,j] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i,j] = 1 + alpha[i][j];
                    }
                    else
                    {
                        matrix[i,j] = alpha[i][j];
                    }
                }
                matrix[i,n-1] = g[i];
            }

            double[] q = SimpleIterat(matrix, n);

            double[] yp = new double[ytoch.Count];
            for (int i = 0; i < ytoch.Count; i++)
            {
                yp[i] = 0;
            }

            for (int i = 0; i < x.Count; i++)
            {
                double sm = 0;
                for (int j = 0; j < n; j++)
                {
                    sm += q[j] * ai(x[i], j);
                }
                yp[i] = f(x[i]) - sm;
            }

            double[] eps = new double[x.Count];
            for (int i = 0; i < x.Count; i++)
            {
                eps[i] = Math.Abs(ytoch[i] - yp[i]);
            }

            Console.Write("x: ");
            foreach (var item in x)
            {
                Console.Write(Math.Round(item, 2) + "  ");
            }
            Console.WriteLine();
            Console.Write("Yt: ");
            foreach (var item in ytoch)
            {
                Console.Write(Math.Round(item,2) + "  ");
            }
            Console.WriteLine();
            Console.Write("Ym: ");
            foreach (var item in yp)
            {
                Console.Write(Math.Round(item, 2) + "  ");
            }
            Console.WriteLine();
            Console.Write("eps: ");
            foreach (var item in eps)
            {
                Console.Write(Math.Round(item, 2) + "  ");
            }
            Console.WriteLine();
        }
    }
}
