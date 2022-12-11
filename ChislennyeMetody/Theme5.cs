using System;

namespace ChislennyeMetody
{
    public static class Integration
    {
        public static double Rectangle(Func<double, double> f, double a, double b, int n)
        {
            double sum1 = LeftRectangle(f, a, b, n);
            double sum2 = RightRectangle(f, a, b, n);

            return (sum1+sum2)/2;
        }

        public static double LeftRectangle(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0d;
            for (int i = 0; i <= n - 1; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }

            sum *= h;
            return sum;
        }

        public static double RightRectangle(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0d;
            for (int i = 1; i <= n; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }

            sum *= h;
            return sum;
        }
        public static double Trapeze(Func<double, double> f, double a, double b, int n)
        {

            double res = (f(b) + f(a))/2, x;

            double h = (b - a) / n;

            for (x = a+h; x < b; x += h)
            {
                res += (f(x));
            }
            
            res *= h;
            return res;
        }
        public static double Simpson(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum1 = 0d;
            double sum2 = 0d;

            for(int i = 1; i < n; i+=2)
            {
                sum1 += f(a + i * h);
            }
            for (int i = 2; i < n; i += 2)
            {
                sum2 += f(a + i * h);
            }

            double result = h / 3 * (f(a) + f(b) + 4*sum1 + 2 *sum2);
            return result;
        }
    }
}
