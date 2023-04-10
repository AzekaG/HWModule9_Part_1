using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Создайте набор методов для работы с массивами:
■ Метод для получения всех четных чисел в массиве;
■ Метод для получения всех нечетных чисел в массиве;
■ Метод для получения всех простых чисел в массиве;
■ Метод для получения всех чисел Фибоначчи в массиве.
Используйте механизмы делегатов.*/

namespace HWModule9_Part_1
{
    internal class Program
    {


        delegate int[] GiveValues(int[] values);
        static void Main(string[] args)
        {
            int[] myArray = { 1, 2, 3, 4, 5, 6, 7, 8, 333, 112, 20, 53 ,33 , 13 , 8};
          
            GiveValues[] gives = new GiveValues[] { GiveEven, GiveOdd , isPrime , MakeFibs };
            foreach (var i in gives)
            {
               
                foreach (var j in i.Invoke(myArray))
                {
                    Console.Write(j+" ");
                }
                Console.WriteLine();
            }

            }

        public static int[] GiveEven(int[] values) 
        {
            return values.Where(i=>(i%2 == 0)).ToArray();
        }
        public static int[] GiveOdd(int[] values)
        {
            return values.Where(i => (i % 2 != 0)).ToArray();
        }
        public static int[] isPrime(int[] values)
        { 
            List<int> values1 = new List<int>();
            
            foreach(int a in values)
            {
                if(isPrime(a))
                    values1.Add(a);
            }

            return values1.ToArray();
        }
        public static bool isPrime(int a)       //возвращает тру если число прайм
        {
            bool isPrime = true;
            if (a == 0 || a == 1) { return false; }
            for (int i = 2; i <= a/2; i++)
            {
                if (a % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

                return isPrime;
        }
       
        public static int[] MakeFibs(int[] ints)
        {
            List<int> fibs = new List<int>();
            List<int> res = new List<int>();
            int a = 0;
            int b = 1;
            fibs.Add(a);
            fibs.Add(b);
            while(a<= Int32.MaxValue-b)     //создаем ряд фибоначи , столько сколько вместит инт
            {
                (a, b) = (b, a + b);
                fibs.Add(b);
            }

            foreach(int i in ints)      //проверяем есть ли числа Фибоначи в массиве , если да - записываем в новый лист 
            {
                if (!fibs.Contains(i)) continue;
                res.Add(i);
            }
            return res.ToArray();
        }
    
      
    }
}
