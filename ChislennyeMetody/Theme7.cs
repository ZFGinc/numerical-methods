namespace ChislennyeMetody
{
    public static class LeastSquareMethod
    {
        public static double[] Linear(double[][] XYdata) 
        {
            int n = XYdata[0].Length;

            double Mx = 0;
            double Mxx = 0;
            double My = 0;
            double Mxy = 0;

            foreach (double num in XYdata[0]) Mx += num;
            Mx /= n;

            foreach (double num in XYdata[0])
            {
                Mxx += num * num;
            }
            Mxx /= n;

            foreach (double num in XYdata[1]) My += num;
            My /= n;

            for(int i = 0; i < n; i++)
            {
                double y = XYdata[1][i];
                double x = XYdata[0][i];

                Mxy += x*y;
            }
            Mxy /= n;

            double[,] Matrix = new double[2, 3]
            {
                {Mxx, Mx, Mxy},
                {Mx, 1, My}
            };

            GaussMethod.matrix = Matrix;
            GaussMethod.n = 2;

            return GaussMethod.GetSolve();
        }
        public static double[] Quadratic(double[][] XYdata)
        {
            int n = XYdata[0].Length;

            double Mx = 0;
            double Mx2 = 0;
            double Mx3 = 0;
            double Mx4 = 0;

            double My = 0;

            double Mxy = 0;
            double Mx2y = 0;

            foreach (double num in XYdata[0])
            {
                Mx += num;
                Mx2 += num * num;
                Mx3 += num * num * num;
                Mx4 += num * num * num * num;
            }
            Mx /= n;
            Mx2 /= n;
            Mx3 /= n;
            Mx4 /= n;

            foreach (double num in XYdata[1]) My += num;
            My /= n;

            for (int i = 0; i < n; i++)
            {
                double y = XYdata[1][i];
                double x = XYdata[0][i];

                Mxy += x*y;
                Mx2y += x*x*y;
            }
            Mxy /= n;
            Mx2y /= n;

            double[,] Matrix = new double[3, 4]
            {
                {Mx4, Mx3, Mx2, Mx2y},
                {Mx3, Mx2, Mx, Mxy},
                {Mx2, Mx, 1, My},
            };

            GaussMethod.matrix = Matrix;
            GaussMethod.n = 3;

            return GaussMethod.GetSolve();
        }
    }
}
