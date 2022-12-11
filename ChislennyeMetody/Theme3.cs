using System;
using System.Windows.Forms;

namespace ChislennyeMetody
{
    public static class GaussMethod
    {
        public static double[,] matrix = default;
        public static int n = default;

        public static double[] GetSolve()
        {
            if (matrix == default || n == default) return new double[] { double.NaN };
            RefrashMatrix();

            double[] x = new double[n];

            for (int k = 1; k < n; k++)
            {
                for (int j = k; j < n; j++)
                {
                    double m = matrix[j, k - 1] / matrix[k - 1, k - 1];
                    for (int i = 0; i < n + 1; i++)
                    {
                        matrix[j, i] = matrix[j, i] - m * matrix[k - 1, i];
                    }
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = matrix[i, n] / matrix[i, i];
                for (int c = n - 1; c > i; c--)
                {
                    x[i] = x[i] - matrix[i, c] * x[c] / matrix[i, i];
                }
            }

            return x;
        }

        private static bool CheckZeroDeterminant()
        {
            if (matrix[0, 0] == 0) return true;
            for (int i = 0; i < n; i++)
            {
                if (matrix[i, i] != 0) return false;
            }
            return true;
        }

        private static void RefrashMatrix()
        {
            Random random = new Random();
            double[] temp = new double[n + 1];
            while (CheckZeroDeterminant())
            {
                int d = random.Next(1, n);

                for (int i = 0; i < n + 1; i++)
                    temp[i] = matrix[d, i];

                for (int i = 0; i < n + 1; i++)
                {
                    matrix[d, i] = matrix[0, i];
                    matrix[0, i] = temp[i];
                }
            }
        }
    }

    public static class ZeidelMethod
    {
        public static double[,] Matrix;
        public static int mSize;
        private static double error = 0;

        public static double[] GetSolve()
        {
            double[] result = new double[mSize];
            prepareNorm();
            double matrNorm = norm();

            if (matrNorm >= 1)
            {
                MessageBox.Show("Условие сходимости не было достигнуто");
                return null;
            }
            else if (matrNorm >= 0.5)
            {
                error *= (1 - matrNorm) / matrNorm;
            }


            double[] ds = new double[mSize];
            for (int i = 0; i < mSize; i++)
            {
                result[i] = Matrix[i, mSize];
                ds[i] = Matrix[i, mSize];
            }

            double[] prev = new double[result.Length];

            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    prev[i] = result[i];
                }

                for (int i = 0; i < mSize; i++)
                {
                    double temp = 0;

                    for (int j = 0; j < mSize; j++)
                    {
                        temp += result[j] * Matrix[i, j];
                    }

                    result[i] = temp + ds[i];
                }

            } while (Math.Abs(vekN(result) - vekN(prev)) > error);

            return result;
        }

        public static double vekN(double[] a)
        {
            double sum = 0;

            for (int i = 0; i < a.Length; i++)
            {
                sum += Math.Abs(a[i]);
            }

            return sum;
        }

        private static double norm()
        {
            double res = 0, tempsum;

            for (int i = 0; i < mSize; i++)
            {
                tempsum = 0;

                for (int j = 0; j < mSize; j++)
                    tempsum += Math.Abs(Matrix[i, j]);
                
                if (tempsum > res)
                    res = tempsum;
            }

            for (int i = 0; i < mSize; i++)
            {
                tempsum = 0;

                for (int j = 0; j < mSize; j++)
                    tempsum += Math.Abs(Matrix[j, i]);
                
                if (tempsum > res)
                    res = tempsum;
            }

            return res;
        }

        private static void prepareNorm()
        {
            double[] diagMatr = new double[mSize + 1];

            for (int i = 0; i < mSize; i++)
            {
                diagMatr[i] = Matrix[i, i];
            }

            for (int i = 0; i < mSize; i++)
            {
                for (int j = 0; j < mSize; j++)
                {
                    if (i == j)
                    {
                        Matrix[i, j] = 0;
                    }
                    else
                    {
                        Matrix[i, j] /= -diagMatr[i];
                    }
                }

                Matrix[i, mSize] /= diagMatr[i];
            }
        }
    }
}