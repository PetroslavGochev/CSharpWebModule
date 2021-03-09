using System;
using System.Diagnostics;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal money = 0;
            var objLock = new object();
            var thread1 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (objLock)
                    {
                        money++;
                    }
                }
            });
            thread1.Start();

            var thread2 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (objLock)
                    {
                        money++;
                    }
                    
                }
            });

            thread2.Start();

            var thread3 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (objLock)
                    {
                        money++;
                    }
                }
            });

            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            Console.WriteLine(money);
        }

        private static void MyThreadMainMethod()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine(CountPrimeNumber(1, 10000000));
            Console.WriteLine(sw.Elapsed);
        }

        private static int CountPrimeNumber(int from, int to)
        {
            int count = 0;
            for (int i = from; i <= to; i++)
            {
                bool isPrime = true;
                for (int div = 2; div < Math.Sqrt(i); div++)
                {
                    if (i % div == 0)
                    {
                        isPrime = false;
                    }
                }
                if (isPrime)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
