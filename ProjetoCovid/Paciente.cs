using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class Paciente
    {
        public int Senha { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Relatorio Relatorio { get; set; }
        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }

        public Paciente(string cPF, string nome, DateTime dataNascimento, Relatorio relatorio)
        {
            CPF = cPF;
            Nome = nome;
            DataNascimento = dataNascimento;
            Relatorio = relatorio;
            Proximo = null;
            Anterior = null;
        }
        
        public Paciente(Paciente paciente, Relatorio relatorio)
        {
            CPF = paciente.CPF;
            Nome = paciente.Nome;
            DataNascimento = paciente.DataNascimento;
            Relatorio = relatorio;
            Proximo = null;
            Anterior = null;

        }
        public Paciente(string nome, string cPF, DateTime dataNascimento)
        {
            CPF = cPF;
            Nome = nome;
            DataNascimento = dataNascimento;
            Proximo = null;
            Anterior = null;
        }
        public Paciente(int senha)
        {
            Senha = senha;
        }
        public override string ToString()
        {
            return "Nome: " + Nome + " CPF " + CPF + " Data de Nascimento " + (DataNascimento).ToString("d/M/yyyy");
        }
    }
}