using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImplicitOperatorLancamentoTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<string> file = File.ReadLines("./teste-01.txt");

                //converte as linhas em objetos do tipo Lancamento
                IEnumerable<Lancamento> lancamentos = file.Select(linha => (Lancamento)linha).ToList();

                // lancamentos.Add(new Lancamento(DateTime.Now, "999", 0.01M, @"Olha o teste", "gusceme"));

                //Converte os objetos do tipo Lancamento de volta para linhas (string)
                IEnumerable<string> linhas = lancamentos.Select(lancamento => (string)lancamento);

                File.WriteAllLines("./teste-02.txt", linhas);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Arquivo fora do layout, tratar...");
                throw e;
            }
        }
    }
}
