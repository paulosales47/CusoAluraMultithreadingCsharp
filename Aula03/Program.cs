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

            //Corrida();

            //Task primeiraTarefa = Task.Run(() => PrimeiraTarefa());
            //Task segundaTarefa = new Task(() => SegundaTarefa());
            //Task terceiraTarefa = new Task(() => TerceiraTarefa());


            //primeiraTarefa.ContinueWith((tarefaAnterior) => segundaTarefa.Start(), 
            //    TaskContinuationOptions.NotOnFaulted);

            //segundaTarefa.ContinueWith((tarefaAnterior) => terceiraTarefa.Start(),
            //    TaskContinuationOptions.NotOnFaulted);

            ////AÇÕES EXECUTADAS SOMENTE EM CASO DE ERRO
            //primeiraTarefa.ContinueWith((tarefaAnterior) => ExibirErroTarefa(tarefaAnterior),
            //    TaskContinuationOptions.OnlyOnFaulted);

            //segundaTarefa.ContinueWith((tarefaAnterior) => ExibirErroTarefa(tarefaAnterior),
            //    TaskContinuationOptions.OnlyOnFaulted);

            //terceiraTarefa.Wait();


            Task tarefaMae = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Tarefa mãe inicio");

                for (int i = 0; i < 10; i++)
                {
                    int IdTarefa = i;
                    Task tarefaFilha = Task.Factory.StartNew((Id) => 
                    ExecutarTarefa(IdTarefa),
                    IdTarefa,
                    TaskCreationOptions.AttachedToParent);
                }
            });

            tarefaMae.Wait();
            Console.WriteLine("Tarefa mãe terminou");

            watch.Stop();
            Console.WriteLine($"Tempo total: {watch.ElapsedMilliseconds}ms");

        }

        private static void ExibirErroTarefa(Task tarefaAnterior)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Erro ocorrido na tarefa:");
            foreach (var erro in tarefaAnterior.Exception.InnerExceptions)
            {
                Console.WriteLine(erro);
            }
        }

        private static void TerceiraTarefa()
        {
            Console.WriteLine("Terceira tarefa");
        }

        private static void SegundaTarefa()
        {
            Console.WriteLine("Segunda tarefa");
            throw new ApplicationException("Erro ao executar segunda tarefa");
        }

        private static void PrimeiraTarefa()
        {
            Console.WriteLine("Primeira tarefa");
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
            Console.WriteLine($"\tInicio da tarefa {idTarefa}");
            Thread.Sleep(1000);
            Console.WriteLine($"\tFim da tarefa {idTarefa}");
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
