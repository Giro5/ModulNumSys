using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulOtherNumberSystem
{
    class Program
    {
        public static NumSys c1, c2, c3;
        public static void Definition()
        {
            string str;
            bool b = true;
            while (b)
            {
                Console.Write("Введите первое число, а через пробел его систему счисления: ");
                str = Convert.ToString(Console.ReadLine());
                b = false;
                c1 = new NumSys { n = str.Split(' ')[0] };
                try
                {
                    c1.i = Convert.ToInt32(str.Split(' ')[1]);
                }
                catch
                {
                    c1.i = 10;
                }
                for (int i = 0; i < c1.n.Length; i++)
                {
                    if ((!char.IsLetter(c1.n[i]) && Convert.ToInt32(c1.n[i].ToString()) >= c1.i) || c1.n[i] - 55 >= c1.i)
                    {
                        b = true;
                        Console.WriteLine("Число введенно не корректно, повторите ввод.");
                    }
                }
            }
            b = true;
            while (b)
            {
                Console.Write("Введите второе число, а через пробел его систему счисления: ");
                str = Convert.ToString(Console.ReadLine());
                b = false;
                c2 = new NumSys { n = str.Split(' ')[0] };
                try
                {
                    c2.i = Convert.ToInt32(str.Split(' ')[1]);
                }
                catch
                {
                    c2.i = 10;
                }
                for (int i = 0; i < c2.n.Length; i++)
                {
                    if ((!char.IsLetter(c2.n[i]) && Convert.ToInt32(c2.n[i].ToString()) >= c2.i) || c2.n[i] - 55 >= c2.i)
                    {
                        b = true;
                        Console.WriteLine("Число введенно не корректно, повторите ввод.");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            for (Definition(); ;)
            {
                Console.Clear();
                Console.WriteLine($"А) {c1.n} в СС {c1.i}");
                Console.WriteLine($"В) {c2.n} в СС {c2.i}");
                Console.WriteLine("Введите код операции для ее исполнения, через пробел укажите желаймую систему счисления.");
                Console.Write("1 - Сумма двух чисел\n2 - Вычитание двух чисел\n3 - Умножение двух чисел\n4 - Деление двух чисел\n5 - Перевод в любую систему счисления\n6 - Отношение двух чисел\n7 - Изменить числа\nВаш код - ");
                string str = Convert.ToString(Console.ReadLine());
                c3 = new NumSys { n = str.Split(' ')[0] };
                try
                {
                    c3.i = Convert.ToInt32(str.Split(' ')[1]);
                }
                catch
                {
                    c3.i = 10;
                }
                switch (c3.n)
                {
                    case "1":
                        Console.WriteLine($"Сумма двух чисел равна: {c1.ToAnySys(c1.Sum(c1, c2), c3.i).n} в СС {c3.i}");
                        break;
                    case "2":
                        Console.WriteLine($"Вычитание двух чисел равно: {c1.ToAnySys(c1.Subtract(c1, c2), c3.i).n} в СС {c3.i}");
                        break;
                    case "3":
                        Console.WriteLine($"Умножение двух чисел равно: {c1.ToAnySys(c1.Multiplication(c1, c2), c3.i).n} в СС {c3.i}");
                        break;
                    case "4":
                        Console.WriteLine($"Деление двух чисел равно: {c1.ToAnySys(c1.Divide(c1, c2), c3.i).n} в СС {c3.i}");
                        break;
                    case "5":
                        Console.WriteLine($"Числа в {c3.i} системе счисления равны: {c1.ToAnySys(c1, c3.i).n}, {c2.ToAnySys(c2, c3.i).n}");
                        break;
                    case "6":
                        Console.WriteLine($"Отношения двух чисел таковы:\nA == B - {c1.Eq(c1, c2)}\nA != B - {c1.NoEq(c1, c2)}\nA >= B - {c1.GrEq(c1, c2)}\nA <= B - {c1.LsEq(c1, c2)}\nA > B - {c1.Gr(c1, c2)}\nA < B - {c1.Ls(c1, c2)}");
                        break;
                    case "7":
                        Definition();
                        break;
                }
                Console.Write("Для продолжения нажмите любую кнопку.");
                Console.ReadKey();
            }
        }

    }
    class NumSys /*SolutionForAnyNumeralSystem*/
    {
        public int i;
        private string N;
        public string n
        {
            get { return N; }
            set
            {
                N = value;
                for (int i = 0; i < N.Length; i++)
                    if (char.IsLower(N[i]))
                        N = N.Replace(N[i], (char)(N[i] - 32));
            }
        }
        public NumSys ToDec(NumSys a)
        {
            if (a.i == 10)
                return a;
            double res = 0;
            for (int i = 0; i < a.n.Length; i++)
                if (!char.IsLetter(a.n[i]))
                    res += Convert.ToDouble(a.n[i].ToString()) * Math.Pow(a.i, a.n.Length - i - 1.0);
                else
                    res += (a.n[i] - 55) * Math.Pow(a.i, (double)a.n.Length - i - 1);
            return new NumSys { n = res.ToString(), i = 10 };
        }
        public NumSys ToAnySys(NumSys a, int newbase)
        {
            if (newbase == 10)
                return new NumSys { n = ToDec(a).n, i = 10 };
            string str = null;
            FromDecToAny(Convert.ToInt32(ToDec(a).n), newbase, ref str);
            return new NumSys { n = str, i = newbase };
        }
        public void FromDecToAny(int a, int newbase, ref string str)
        {
            if (a >= newbase)
                FromDecToAny(a / newbase, newbase, ref str);
            int tmp = a % newbase;
            if (tmp <= 9)
                str += tmp;
            else
                str += (char)(tmp + 55);
        }
        public NumSys Sum(NumSys a, NumSys b)
        {
            return new NumSys { n = (Convert.ToDouble(ToDec(a).n) + Convert.ToDouble(ToDec(b).n)).ToString(), i = 10 };
        }
        public NumSys Subtract(NumSys a, NumSys b)
        {
            double res = Convert.ToDouble(ToDec(a).n) - Convert.ToDouble(ToDec(b).n);
            return new NumSys { n = res > 0 ? res.ToString() : "0", i = 10 };
        }
        public NumSys Multiplication(NumSys a, NumSys b)
        {
            return new NumSys { n = (Convert.ToDouble(ToDec(a).n) * Convert.ToDouble(ToDec(b).n)).ToString(), i = 10 };
        }
        public NumSys Divide(NumSys a, NumSys b)
        {
            return new NumSys { n = (Convert.ToInt32(ToDec(a).n) / Convert.ToInt32(ToDec(b).n)).ToString(), i = 10 };
        }
        public bool Eq(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) == Convert.ToDouble(ToDec(b).n);
        }
        public bool NoEq(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) != Convert.ToDouble(ToDec(b).n);
        }
        public bool GrEq(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) >= Convert.ToDouble(ToDec(b).n);
        }
        public bool LsEq(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) <= Convert.ToDouble(ToDec(b).n);
        }
        public bool Gr(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) > Convert.ToDouble(ToDec(b).n);
        }
        public bool Ls(NumSys a, NumSys b)
        {
            return Convert.ToDouble(ToDec(a).n) < Convert.ToDouble(ToDec(b).n);
        }
    }
}
