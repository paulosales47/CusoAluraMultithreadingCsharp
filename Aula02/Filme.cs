using System;
using System.Collections.Generic;
using System.Text;

namespace Aula02
{
    public class Filme
    {
        public string Titulo { get; set; }
        public decimal Faturamento { get; set; }
        public decimal Orcamento { get; set; }
        public string Distribuidor { get; set; }
        public string Genero { get; set; }
        public string Diretor { get; set; }
        public decimal Lucro { get; set; }
        public decimal LucroPorcentagem { get; set; }

        public int CompareTo(object obj)
        {
            Filme outro = obj as Filme;
            return Titulo.CompareTo(outro.Titulo);
        }
    }
}
