using System;
using System.Collections.Generic;

namespace ChislennyeMetody
{
    public static class DifferentialEquations
    {
        public static double[][] Euler(Func<double, double,double> f, double a, double b, double startY, int n)
        {
            double[] Xi = new double[n + 1];
            double[] Yi = new double[n + 1];

            double h = (b - a) / n;

            for (int i = 0; i < n; i++)
            {
                Xi[i] = i * h + a;
            }

            Yi[0] = startY;
            Xi[n] = b;


            double[] deltaYs = new double[n + 1];
            deltaYs[0] = h * f(Xi[0], Yi[0]);
            for (int i = 1; i <= n; i++)
            {
                Yi[i] = Yi[i - 1] + deltaYs[i - 1];
                deltaYs[i] = h * f(Xi[i], Yi[i]);
            }

            return new double[][] { Xi, Yi };
        }

        public static double[][] ImprovedEuler(Func<double, double, double> f, double a, double b, double startY, int n) 
        {
            double[] xOT = new double[n]; 
            double[] yOT = new double[n];

            double[] Xi = new double[n + 1];
            double[] Yi = new double[n + 1];

            double h = (b - a) / n;

            for (int i = 0; i < n; i++)
            {
                Xi[i] = i * h + a;
                xOT[i] = Xi[i] + (h / 2);
            }

            Yi[0] = startY;
            Xi[n] = b;

            for (int i = 1; i <= n; i++)
            {
                yOT[i - 1] = Yi[i - 1] + f(Xi[i - 1], Yi[i - 1]) * h / 2;
                Yi[i] = Yi[i - 1] + f(xOT[i - 1], yOT[i - 1]) * h;
            }

            return new double[][] { Xi, Yi };
        }

        public static double RungeKutta(Func<double, double, double> f, double x0, double y0, double h, double x)
        {
            double xnew, ynew, k1, k2, k3, k4, result = double.NaN;
            if (x == x0)
                result = y0;
            else if (x > x0)
            {
                do
                {
                    if (h > x - x0) h = x - x0;
                    k1 = h * f(x0, y0);
                    k2 = h * f(x0 + 0.5 * h, y0 + 0.5 * k1);
                    k3 = h * f(x0 + 0.5 * h, y0 + 0.5 * k2);
                    k4 = h * f(x0 + h, y0 + k3);
                    ynew = y0 + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                    xnew = x0 + h;
                    x0 = xnew;
                    y0 = ynew;
                } while (x0 < x);
                result = ynew;
            }
            return result;
        }
    }
}
