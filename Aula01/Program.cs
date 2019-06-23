using System;
using System.Diagnostics;
using System.Linq;
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
            //Paralelo(); //5092

            //Executar100TarefasSerial(); //10163
            //Executar100TarefasParalelo(); //934
            //ExecutarColecaoParalelo(); //1058
            FinalizandoLoop();

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

        public static void Executar100TarefasSerial()
        {
            Console.WriteLine("Processando 100 itens em série");
            for (int i = 0; i < 100; i++)
            {
                Processar(i);
            }

        }

        public static void Executar100TarefasParalelo()
        {
            Console.WriteLine("Processando 100 itens em paralelo");
            Parallel.For(0, 100, (i) => Processar(i));
        }

        public static void Processar(object item)
        {
            Console.WriteLine($"Processando item: {item}");
            Thread.Sleep(100);
            Console.WriteLine($"Finalizando processamento do item: {item}");
        }

        public static void ExecutarColecaoParalelo()
        {
            var itens = Enumerable.Range(0, 100);
            Parallel.ForEach(itens, (item) => Processar(item));
        }

        public static void FinalizandoLoop()
        {
            ParallelLoopResult loop = Parallel.For(0, 100, (int i, ParallelLoopState state) => 
            {
                if (i == 75)
                    state.Break();
                
                Processar(i);
            });

            Console.WriteLine($"Processado sem interrupção: {loop.IsCompleted}");
            Console.WriteLine($"Itens processado: {loop.LowestBreakIteration}");
        }

    }
}
