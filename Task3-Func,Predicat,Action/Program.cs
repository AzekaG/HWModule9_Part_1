using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Console;

namespace Task3_Func_Predicat_Action
{/*Создайте набор методов:
■ Метод для отображения текущего времени;
■ Метод для отображения текущей даты;
■ Метод для отображения текущего дня недели;
■ Метод для подсчёта площади треугольника;
■ Метод для подсчёта площади прямоугольника.
Для реализации проекта используйте делегаты Action,
Predicate, Func.*/




    internal class Program
    {
        public delegate double MyDelegateArea(double A, double B, double C);
        public delegate double MyDelegateArea2(double A, double B);
        public delegate void DateAction();
         class MyDateNow
        {
            public void NowTime() 
            {   
                DateTime date = DateTime.Now;
                Console.Write(date.ToShortTimeString());
                Console.Write("  ");
            }

            public void NowDate() {
                DateTime dt = DateTime.Now;
                Console.Write("{0}.", dt.ToShortDateString());
                Console.Write("  ");
            }
            public void NowDay()
            {
                DateTime dt = DateTime.Now;
                Console.Write("{1}.", dt, dt.DayOfWeek);
                Console.Write("  ");
            }
        }

      
        class Triangle
        {
          
            public double Area(double A , double B , double C)
            {
                double P = (A+B+C)/2;
                return (double)Math.Sqrt(P * (P - A) * (P - B) * (P - C));

            }

        }
        class Square
        {

            public double Area(double A, double B )
            {
                return A*B;
            }
        }
            static void Main(string[] args)
            {

            Console.Write("Date now is : ");
                MyDateNow myDateNow = new MyDateNow();
                DateAction date = myDateNow.NowDate;
                
               date += myDateNow.NowDay;
               date += myDateNow.NowTime;
                date();

            Console.WriteLine();
            Console.WriteLine("Triangle : enter A,B,C : ");
            double a, b, c;
            Console.WriteLine("Enter a : ");
            a = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter b : ");
            b = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter c : ");
            c = double.Parse(Console.ReadLine());
            Console.WriteLine("Suare : enter A,B : ");
            
            Console.WriteLine("Enter A : ");
            int A = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter B : ");
            int B = int.Parse(Console.ReadLine());
            Triangle triangle = new Triangle();
            Square square = new Square();
            MyDelegateArea myDelegateArea = triangle.Area;
            MyDelegateArea2 myDelegateArea2 = square.Area;
            
           Console.WriteLine("Area of Triangle " +myDelegateArea(a,b,c));
            Console.WriteLine("Area of Square : " + myDelegateArea2(A, B));






            }


        }
    }

