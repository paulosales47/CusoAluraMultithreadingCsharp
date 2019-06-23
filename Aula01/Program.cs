using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Aula01
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = Stopwatch.StartNew();

            //Serial(); //10020
            Paralelo(); //5092

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        public static void Serial()
        {
            Console.WriteLine("Executando tarefas de forma serial");
            Tarefa1();
            Tarefa2();
        }

        public static void Paralelo()
        {
            Console.WriteLine("Executando tarefas de forma paralela");
            Parallel.Invoke(
                () => Tarefa1(),
                () => Tarefa2());
        }

        public static void Tarefa1()
        {
            Console.WriteLine("Executando tarefa 1...");
            Thread.Sleep(5000);
        }

        public static void Tarefa2()
        {
            Console.WriteLine("Executando tarefa 2...");
            Thread.Sleep(5000);
        }
    }
}
