using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Aula03
{
    public class Program
    {
        static void Main(string[] args)
        {


            Stopwatch watch = Stopwatch.StartNew();

            Task tarefa1 = new Task(() => ExecutarTarefa(1));
            tarefa1.Start();
            tarefa1.Wait();

            Task tarefa2 = Task.Run(() => ExecutarTarefa(2));
            tarefa2.Wait();


            Task<int> tarefa3 = Task.Run(() => ExecutarTarefaSoma(1, 3));
            Console.WriteLine($"Resultado da soma {tarefa3.Result}");


            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            
        }


        public static void ExecutarTarefa(int idTarefa)
        {
            Console.WriteLine($"Inicio da tarefa {idTarefa}");
            Thread.Sleep(2000);
            Console.WriteLine($"Fim da tarefa {idTarefa}");
        }

        public static int ExecutarTarefaSoma(int arg1, int arg2)
        {
            Console.WriteLine($"Inicio da tarefa de soma");
            Thread.Sleep(2000);
            Console.WriteLine($"Fim da tarefa de soma");
            return arg1 + arg2;
        }



    }
}
