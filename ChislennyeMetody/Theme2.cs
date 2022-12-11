using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChislennyeMetody
{
    /// Тема №2
    /// 

    //Класс хронящий диапозон
    //Просто класс
    public class SolveRange
    {
        public SolveRange()
        {
            
        }
        public SolveRange(double x0, double x1)
        {
            this.x0 = x0;
            this.x1 = x1;
        }

        public double x0;
        public double x1;
    }

    //Класс служащий общим объектом для просчета функции, производной, поиска диапозонов
    //и хранения информации для всех классов в работе методов
    public static class GenT2
    {
        //Параметры функции 3 степени Y = ax^3+bx^2+cx+d 
        public static double a, b, c, d;

        //Точность... просто точноть
        public static double eps;

        //Начало, конец и шаг для поиска пересечений графика функции через ось ОХ
        private static double StartX = -20;
        private static double EndX = 20;
        private static double StepToFind = 0.1;

        //Список хронящий в себе все диапозоны пересечений
        public static List<SolveRange> Ranges = new List<SolveRange>();
        //Сама функция для возврата значения 
        public static double f(double x) => a * Math.Pow(x,3) + b * Math.Pow(x, 2) + c * x + d;
        //Производная функции
        public static double DerivativeF(double x) => 3 * a * Math.Pow(x, 2) + 2 * b * x + c;
        //Вторая производная функции
        public static double Derivative2f(double x) => 6 * a * x + 2 * b;
        //Функция сжатия
        public static double fi(double x)
        {
            double y = 0;
            if (c != 0) y = (-a * x * x * x - b * x * x - d) / c;
            else if (b != 0) y = (-a * x * x * x - d) / (b * x);
            else if (a != 0) y = -d/(a*x*x);
            return y;
        }

        //2 метода служащие установкой необходимых параметров
        public static void SetParams(double A, double B, double C, double D)
        {
            a = A;
            b = B;
            c = C;
            d = D;
        }
        public static void SetParams(double[] args)
        {
            a = args[0];
            b = args[1];
            c = args[2];
            d = args[3];
        }
        //Метод находящий диапозоны, где график функции пересекает ось ОХ
        public static void GetRanges()
        {
            Ranges.Clear();

            double x = StartX;

            while (x < EndX)
            {
                if (f(x) * f(x + StepToFind) <= 0) Ranges.Add(new SolveRange(x, x + StepToFind));

                x += StepToFind;
            }
        }
        //Метод находящий диапозоны по экстремумам функции
        public static void GetRangesForIters()
        {
            Ranges.Clear();
            List<double> Extrem = GetExtremums();
            for(int i = 0; i < Extrem.Count; i++)
            {
                if(i == 0) 
                    Ranges.Add(new SolveRange(StartX, Extrem[0]));
                else
                    Ranges.Add(new SolveRange(Extrem[i-1], Extrem[i]));
            }
            Ranges.Add(new SolveRange(Extrem[Extrem.Count-1], EndX));
        }
        //Метод находящий экстремумы функции
        private static List<double> GetExtremums()
        {
            List<double> ret = new List<double>();

            double D = 4 * b * b - 12 * a * c;
            if (a == 0) ret.Add(-c / (2 * b));
            if (D > 0 && a != 0)
            {
                double sD = Math.Sqrt(D);
                ret.Add((-2 * b - sD) / (6 * a));
                ret.Add((-2 * b + sD) / (6 * a));
            }
            else if (D == 0) ret.Add((-1 * b) / (3 * a));

            return ret;
        }
    }

    //Перед началом работы с любыми классами нобходимо:
    // 1) Установить точность => переменная eps в классе GenT2
    // 2) Используя класс GenT2 (базовый класс для работы по теме №2) установить коэфициенты => параметры a,b,c,d.
    //    Для этого можно использовать метод SetParams, либо в ручную каждый отдельно.
    public static class Dihotomia
    {
        //Основной метод к которомы нужно обращаться за получением ответа
        //Он возвращает точки Х, где Y = 0
        public static List<double> GetSolve()
        {
            //Создает пустой список для просчитанных точек
            List<double> Solves = new List<double>();
            //Вызываем метод для поиска диапозонов
            GenT2.GetRanges();

            //Начинаем перебирать кажды диапозон и искать приблизительное значение
            foreach (var range in GenT2.Ranges)
            {
                //Вызываем метод поиска значения на определенном участке и записываем его в наш заготовленный список
                Solves.Add(DihotomiaX(range));
            }
            //Избавление от повторений
            Solves = Solves.Distinct().ToList<double>();
            return Solves;
        }

        //Метод, который ищет приблизительные точки в отрезках
        private static double DihotomiaX(SolveRange range)
        {
            //Начальные значения диапазона
            double x0 = range.x0, x1 = range.x1;
            //Первая точка - середина между х0 и х1
            double c = (x0 + x1) / 2;

            do {
                //Проверям, пересекает ли график функции ось ОХ
                double temp = GenT2.f(x0) * GenT2.f(c);

                //Есль мы сразу наткнулись на 0, тогда мы вернем точку, в которой это произошло.
                if (temp == 0)
                {
                    if (GenT2.f(x0) == 0) return x0;
                    else return x1;
                }

                //Дальше идем, если мы не попали в 0
                //Проверка на какой из половин график пересек ось ОХ и сужаем отрезок в нужную сторону
                if (temp < 0) x1 = c;
                else x0 = c;

                //Просчитываем новую середину отрезка
                c = (x0 + x1) / 2;
            } while (Math.Abs(x0-x1) >= GenT2.eps);

            return (x0 + x1) / 2;
        }
    }
    public static class Hords
    {
        //Так же служит для обращения за решением
        public static List<double> GetSolve()
        {
            //Создает пустой список для просчитанных точек
            List<double> Solves = new List<double>();
            //Вызываем метод для поиска диапозонов
            GenT2.GetRanges();

            //Начинаем перебирать кажды диапозон и искать приблизительное значение
            foreach (var range in GenT2.Ranges)
            {
                //Вызываем метод поиска значения на определенном участке и записываем его в наш заготовленный список
                Solves.Add(HordsX(range));
            }
            //Избавление от повторений
            Solves = Solves.Distinct().ToList<double>();
            return Solves;
        }
        //Метод, который ищет приблизительные точки в отрезках
        private static double HordsX(SolveRange range)
        {
            //Начальные значения
            double x0 = range.x0, x1 = range.x1;
            //Ищем вторую производную от функции в определенной точке
            double der2 = GenT2.Derivative2f(x0);

            //Точки ( cn и cn1 ) - пересечения хорд с осью ОХ 
            double cn;
            //Самая первая хорда для всех одинакова
            double cn1 = x0 - ((x1 - x0) / (GenT2.f(x1) - GenT2.f(x0))) * GenT2.f(x0); 

            //Нужно для пометки, какой из концов отрезка должен оставаться неподвижным
            bool leftDotFixed = true;

            //Определение - какая из точек должна быть не подвижной
            //(знак функции и второй произвожной должны быть одинаковыми)
            if (GenT2.f(x0) * der2 < 0)
            {
                cn = x0;
                leftDotFixed = false;
            }
            else
            {
                cn = x1;
                leftDotFixed = false;
            }

            do
            {
                double temp = cn1;
                //Для неподвижной точки слева
                if (leftDotFixed)
                {
                    if (cn == x1) return temp;
                    cn1 = cn - ((x1 - cn) / (GenT2.f(x1) - GenT2.f(cn))) * GenT2.f(cn);
                }
                //Для неподвижной точки справа
                else
                {
                    if (cn == x0) return temp;
                    cn1 = cn - ((cn - x0) / (GenT2.f(cn) - GenT2.f(x0))) * GenT2.f(cn);
                }
                cn = temp;

            } while (Math.Abs(cn1 - cn) > GenT2.eps);
            
            return cn1;
        }
    }
    public static class Tangents
    {
        //Функция для получения решения
        public static List<double> GetSolve()
        {
            //Создает пустой список для просчитанных точек
            List<double> Solves = new List<double>();
            //Вызываем метод для поиска диапозонов
            GenT2.GetRanges();

            //Начинаем перебирать кажды диапозон и искать приблизительное значение
            foreach (var range in GenT2.Ranges)
            {
                //Вызываем метод поиска значения на определенном участке и записываем его в наш заготовленный список
                Solves.Add(TangentsX(range));
            }
            //Избавление от повторений
            Solves = Solves.Distinct().ToList<double>();
            return Solves;
        }
        private static double TangentsX(SolveRange range)
        {
            //Начальные значения отрезка
            double x0 = range.x0, x1 = range.x1;
            //Вторая производная
            double der2 = GenT2.Derivative2f(x0);

            //Точки ( cn и cn1 ) пересечения касательных с осью ОХ 
            double cn;
            double cn1;

            //Определение - какая из точек должна быть не подвижной
            //(знак функции и второй производной должны быть одинаковыми)
            if (GenT2.f(x0) * der2 < 0) cn1 = x0-(GenT2.f(x0)/GenT2.DerivativeF(x0));
            else cn1 = x1 - (GenT2.f(x1) / GenT2.DerivativeF(x1));
            cn = cn1;

            do
            {
                double temp = cn1;
                //Сама формула просчета новых касательных
                cn1 = cn - (GenT2.f(cn) / GenT2.DerivativeF(cn));
                cn = temp;
            } while (Math.Abs(cn1-cn)>GenT2.eps);

            return cn1;
        }
    }
    public static class Iterations
    {
        static double M = 0;
        private static double Fi(double x) => x - GenT2.f(x) * M;

        //Так же нужно обращаться к этой функции за решением
        public static List<double> GetSolve()
        {
            //Создает пустой список для просчитанных точек
            List<double> Solves = new List<double>();
            //Вызываем метод для поиска диапозонов
            GenT2.GetRanges();

            //Начинаем перебирать кажды диапозон и искать приблизительное значение
            foreach (var range in GenT2.Ranges)
            {
                //Вызываем метод поиска значения на определенном участке и записываем его в наш заготовленный список
                Solves.Add(IterationsX(range));
            }
            return Solves;
        }
        private static double IterationsX(SolveRange range)
        {
            //Начальные приближения
            double a = range.x0, b = range.x1;
            double x0 = (a + b) / 2, x1 = 0;

            //Значения производных на концах отрезка
            double min = GenT2.DerivativeF(a);
            double max = GenT2.DerivativeF(b);

            //Меняем при надобности
            if(min>max) { double temp = max;  max = min; min = temp; }

            if (3 * min > max) M = 0.75 / max;
            else M = 1/max;

            do
            {
                //Просчитываем x = fi(x)
                x1 = Fi(x0);
                x0 = x1;

            } while (Math.Abs(x0 - Fi(x0)) > GenT2.eps);

            return x1;
        }
    }
}
