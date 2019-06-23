using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aula02
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Filme> filmes = BuscarFilmes();

            Stopwatch watch = Stopwatch.StartNew();

            //ListarFilmesAventuraSerial(filmes); //208
            //ListarFilmesAventuraParalelo(filmes); //311
            //ListarFilmesAventuraParaleloExecucaoDefault(filmes); //283
            //ListarFilmesAventuraParaleloExecucaoForce(filmes); //254
            //ListarFilmesAventuraParaleloExecucaoForceNivel4(filmes); //250
            //ListarFilmesAventuraParaleloOrdenada(filmes); //303
            //ListarFilmesAventuraParaleloMaiorFaturamento(filmes); //135 //TOP 4
            //ListarFilmesAventuraParaleloComAction(filmes); //297

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        public static IEnumerable<Filme> BuscarFilmes()
        {
            IEnumerable<Filme> filmes =
                JsonConvert.DeserializeObject<IEnumerable<Filme>>
                (File.ReadAllText("filmes.json"));

            var consulta =
                from f in filmes
                select new Filme
                {
                    Titulo = f.Titulo,
                    Faturamento = f.Faturamento,
                    Orcamento = f.Orcamento,
                    Distribuidor = f.Distribuidor,
                    Genero = f.Genero,
                    Diretor = f.Diretor,
                    Lucro = f.Faturamento - f.Orcamento,
                    LucroPorcentagem = (f.Faturamento - f.Orcamento) / f.Orcamento
                };

            return consulta;
        }

        private static void GeraRelatorio(string tituloRelatorio, IEnumerable<Filme> resultado)
        {
            Console.WriteLine("Relatório: {0}", tituloRelatorio);

            Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
                    "Item",
                    "Faturamento",
                    "Orcamento",
                    "Lucro",
                    "% Lucro");
            Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
                    new string('=', 30),
                    new string('=', 20),
                    new string('=', 20),
                    new string('=', 20),
                    new string('=', 10));

            foreach (var item in resultado)
            {
                Console.WriteLine("{0,-30} {1,20:N2} {2,20:N2} {3,20:N2} {4,10:P}",
                    item.Titulo,
                    item.Faturamento,
                    item.Orcamento,
                    item.Lucro,
                    item.LucroPorcentagem);
            }
            Console.WriteLine();
            Console.WriteLine("FIM DO RELATÓRIO: {0}", tituloRelatorio);
        }

        public static void ListarFilmesAventuraSerial(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParalelo(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes.AsParallel()
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloExecucaoDefault(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes.AsParallel()
                .WithExecutionMode(ParallelExecutionMode.Default)
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloExecucaoForce(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes.AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloExecucaoForceNivel4(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes.AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithDegreeOfParallelism(4)
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloOrdenada(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes
                .AsParallel()
                .AsOrdered()
                where "Adventure".Equals(filme.Genero)
                select filme;

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloMaiorFaturamento(IEnumerable<Filme> filmes)
        {
            var consulta =
                (from filme in filmes
                .AsParallel()
                where "Adventure".Equals(filme.Genero)
                orderby filme.Faturamento descending
                select filme).Take(4);

            GeraRelatorio("Aventura", consulta);
        }

        public static void ListarFilmesAventuraParaleloComAction(IEnumerable<Filme> filmes)
        {
            var consulta =
                from filme in filmes
                .AsParallel()
                where "Adventure".Equals(filme.Genero)
                select filme;

            consulta.ForAll((filme) => 
            {
                Console.WriteLine(filme.Titulo);
            });
        }
    }
}
