using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislennyeMetody
{
    //Тема №1
    //

    public static class MyExpX
    {
        public static double MyExpSimple(double x, double eps)
        {
            double y = 1 + x;
            int i = 2;
            double x1 = x;
            double f = 1;
            double a = 1;
            do
            {
                x *= x1;
                f *= i;
                a = x / f;
                y += a;
                i++;
            } while (Math.Abs(a) >= eps);

            return y;
        }

        public static double MyExpRecurr(double x, double eps)
        {
            double i = 1;
            double a = 1;
            double y = a;

            do
            {
                a *= x / i;
                y += a;
                i++;
            } while (Math.Abs(a) >= eps);

            return y;
        }
    }

    public static class MyCosX
    {
        public static double CosX(double x, double eps)
        {
            int i = 1;
            double x1 = 1;
            double f = 1;

            //Приведению Х к диапозону от -2pi до 2pi
            while (Math.Abs(x) > 2 * Math.PI)
            //do
            {
                if (x < 0) x += 2 * Math.PI;
                else x -= 2 * Math.PI;
            }


            //Поиск косинуса
            do
            {
                f *= -Math.Pow(x, 2) / (2 * i * (2 * i - 1));
                x1 += f;
                i++;
            }
            while (Math.Abs(f) > eps);

            return x1;
        }
    }

    public static class MyLn1plusX
    {
        public static double Ln1plusX(double x, double eps)
        {
            //Начальные проверки вхождения Х в область определения
            if (x == -1) return double.NegativeInfinity;
            if (x < -1) return double.NaN;

            //Сокращение аргумента Х, так что 1+х = e^q * s
            int q = 0;
            x += 1;
            while (x > 1)
            {
                x /= Math.E;
                q++;
            }
            x -= 1;

            //Начальные значения

            double f = x;
            double s = 0;
            double pow = x;
            int z = -1;
            int n = 0;

            //|s| < 0
            //Раскладывем ln(1+x) = q + ln(s)
            //Просчитываем ln(s) = ln(1+(s-1))
            //Используется ряд Тейлора
            while (Math.Abs(f) > eps)
            {
                z *= -1;
                f = z * pow / (n + 1);
                s += f;
                pow *= x;
                n++;
            }

            return s + q;
        }
    }

}
