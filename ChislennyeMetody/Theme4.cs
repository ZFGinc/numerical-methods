namespace ChislennyeMetody
{
    //Класс хронящий все значения для решения интерполяции
    public static class AllData
    {
        public static int n;
        public static double[] MasX = null;
        public static double[] MasY = null;
    }

    public static class IntNewton
    {
        public static double GetSolve(double x)
        {
            double[,] mas = new double[AllData.n + 2, AllData.n + 1];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < AllData.n + 1; j++)
                {
                    if (i == 0)
                        mas[i, j] = AllData.MasX[j];
                    else if (i == 1)
                        mas[i, j] = AllData.MasY[j];
                }
            }
            int m = AllData.n;
            for (int i = 2; i < AllData.n + 2; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mas[i, j] = mas[i - 1, j + 1] - mas[i - 1, j];
                }
                m--;
            }

            double[] dy0 = new double[AllData.n + 1];

            for (int i = 0; i < AllData.n + 1; i++)
            {
                dy0[i] = mas[i + 1, 0];
            }

            double res = dy0[0];
            double[] xn = new double[AllData.n];
            xn[0] = x - mas[0, 0];

            for (int i = 1; i < AllData.n; i++)
            {
                double ans = xn[i - 1] * (x - mas[0, i]);
                xn[i] = ans;
            }

            int m1 = AllData.n + 1;
            int fact = 1;
            for (int i = 1; i < m1; i++)
            {
                fact = fact * i;
                res = res + (dy0[i] * xn[i - 1]) / (fact);
            }

            return res;
        }
    }
    public static class IntLagrange
    {
        public static double GetSolve(double x)
        {
            double res = 0f, s1, s2;

            for (int i = 0; i < (AllData.n / 2); i++)
            {
                s1 = 1f; s2 = 1f;
                for (int j = 0; j < (AllData.n / 2); j++)
                {
                    if (i != j)
                    {
                        s1 *= (x - AllData.MasX[j]);
                        s2 *= (AllData.MasX[i] - AllData.MasX[j]);
                    }
                }
                res += AllData.MasY[i] * (s1 / s2);

            }

            return res;
        }
    }
}
