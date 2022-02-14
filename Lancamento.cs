using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ImplicitOperatorLancamentoTxt
{
    public class Lancamento
    {
        public Lancamento(DateTime data, string conta, decimal valor, string historico, string nome)
        {
            Data = data;
            Conta = conta;
            Valor = valor;
            Historico = historico;
            Nome = nome;
        }

        public DateTime Data { get; set; }
        public string Conta { get; set; }
        public string Historico { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }

        public static implicit operator Lancamento(string linha)
        {
            string data = linha.Substring(Layout.DataStart, Layout.DataLength);
            var dataFormatada = DateTime.ParseExact(data, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            string conta = linha.Substring(Layout.ContaStart, Layout.ContaLength);

            string historico = linha.Substring(Layout.HistoricoStart, Layout.HistoricoLength);

            string valor = linha.Substring(Layout.ValorStart, Layout.ValorLength);
            valor = valor.Insert(valor.Length - 2, ",");
            var valorFormatado = Convert.ToDecimal(valor);

            string nome = linha.Substring(Layout.NomeStart);

            return new Lancamento(dataFormatada, conta, valorFormatado, historico, nome);
        }

        public static implicit operator string(Lancamento lancamento)
        {
            var linha = new StringBuilder();
            linha.Append(lancamento.Data.ToString("yyyyMMdd"));
            linha.Append(lancamento.Conta.PadLeft(Layout.ContaLength, '0'));

            Regex digitsOnly = new Regex(@"[^\d]");
            var valor = digitsOnly.Replace(lancamento.Valor.ToString(), "");
            linha.Append(valor.PadLeft(Layout.ValorLength, '0'));

            Regex noLineBreak = new Regex(@"[\r\n\t]");
            var historico = noLineBreak.Replace(lancamento.Historico.Trim(), "");
            linha.Append(historico.PadRight(Layout.HistoricoLength, ' '));

            linha.Append(lancamento.Nome.PadLeft(Layout.NomeLength, ' '));

            return linha.ToString();
        }
    }
}