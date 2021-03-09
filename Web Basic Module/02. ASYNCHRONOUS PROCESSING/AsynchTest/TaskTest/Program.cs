using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {
        static  void Main(string[] args)
        {
            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 100000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 100000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            Task.Run(() =>
            {
                while (true)
                {
                    var result = Console.ReadLine();
                    Console.WriteLine(result);
                }

            });
          
                while (true)
                {
                    Thread.Sleep(100000);
                    Console.WriteLine("Pesho");
                }       

        }
    }
}
