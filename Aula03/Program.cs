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

            //Task tarefa1 = new Task(() => ExecutarTarefa(1));
            //tarefa1.Start();
            //tarefa1.Wait();

            //Task tarefa2 = Task.Run(() => ExecutarTarefa(2));
            //tarefa2.Wait();

            //Task<int> tarefa3 = Task.Run(() => ExecutarTarefaSoma(1, 3));
            //Console.WriteLine($"Resultado da soma {tarefa3.Result}");

            Corrida();

            watch.Stop();
            Console.WriteLine($"Tempo total: {watch.ElapsedMilliseconds}");
            
        }


        public static void Corrida()
        {
            Console.WriteLine($"Numero de Threads inicio: {Process.GetCurrentProcess().Threads.Count}");
            
            Task[] tarefas = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int idTarefa = i;
                tarefas[i] = Task.Run(() => ExecutarTarefa(idTarefa));
            }

            Task.WaitAll(tarefas);
            Console.WriteLine($"Numero de Threads fim: {Process.GetCurrentProcess().Threads.Count}");
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
