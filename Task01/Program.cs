using System;
using System.Threading;

// 1. Написать приложение, считающее в раздельных потоках: 
//      а. факториал числа N, которое вводится с клавиатуры;
//      b. сумму целых чисел до N.

namespace Task01
{
    class Program
    {
        public static void CheckThread()
        {
            var current_thread = Thread.CurrentThread;
            Console.WriteLine("Поток: \"{0}\"(id:{1}) запущен",
                current_thread.Name, current_thread.ManagedThreadId);
        }

        static void Fact(object obj)
        {
            CheckThread();

            int N = (int)obj;
            double result = 1;

            for (int i = 2; i <= N; i++)
            {
                result *= i;
            }
            Console.WriteLine($"{N}! = {result}");
            Console.WriteLine("Поток {0} завершен", Thread.CurrentThread.ManagedThreadId);
        }

        static void Sum(object obj)
        {
            CheckThread();

            int N = (int)obj;
            int result = 0;
            for (int i = 1; i <= N; i++)
            {
                result += i;
            }

            Console.WriteLine($"Сумма от 0 до {N} = {result}");
            Console.WriteLine("Поток {0} завершен", Thread.CurrentThread.ManagedThreadId);
        }

        static void Main(string[] args)
        {
            //int N = 50;
            Console.Write("Введите целое число не больше 100: ");
            int N = int.Parse(Console.ReadLine());

            var fact_thread = new Thread(Fact);
            fact_thread.Name = "Поток факториала";
            fact_thread.Start(N);

            var sum_thread = new Thread(Sum);
            sum_thread.Name = "Поток суммы";
            sum_thread.Start(N);

            Console.ReadKey();
        }
    }
}
