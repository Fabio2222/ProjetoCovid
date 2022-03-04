using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class ListaArquivados
    {

        public Paciente Head { get; set; }
        public Paciente Tail { get; set; }

        public ListaArquivados()
        {
            Head = null;
            Tail = null;
        }
        public void Entrada(Paciente novoPaciente)
        {

            if (Vazio())
            {
                Head = novoPaciente;
                Tail = novoPaciente;

            }
            else
            {
                Tail.Proximo = novoPaciente;
                Tail = novoPaciente;

            }
            GravarArq(novoPaciente);

        }
        public bool Vazio()
        {
            if ((Head == null) && (Tail == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ImprimirListaArquivados()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Lista de Arquivados");
                Console.WriteLine("Pressione uma tecla para voltar ao Menu");
            }
            else
            {
                Paciente aux = Head;
                do
                {
                    Console.WriteLine(aux);
                    Console.WriteLine(aux.Relatorio.ToString());
                    aux = aux.Proximo;
                    Console.ReadKey();
                } while (aux != null);
            }
            Console.ReadKey();
        }

        public void BuscarArquivado()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Lista de Arquivados");
                Console.WriteLine("Pressione uma tecla para voltar ao Menu");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Informe o CPF do paciente que deseja buscar");
                string busca = Console.ReadLine();
                Paciente aux = Head;
                bool cont = false;
                do
                {
                    if (aux.CPF.Contains(busca))
                    {
                        Console.WriteLine(aux.ToString());
                        Console.WriteLine(aux.Relatorio.ToString());
                        cont = true;
                    }
                    aux = aux.Proximo;
                } while (aux != null);
                if (cont == false)
                {
                    Console.WriteLine("Nenhum Paciente Encontrado na Lista de Arquivados");
                    Console.WriteLine("Pressione uma tecla para voltar ao Menu");
                }
                Console.ReadKey();
            }
        }
        void GravarArq(Paciente aux)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Fábio Moreira\\Arquivados.txt", append: true);
                sw.WriteLine($"{aux.CPF};{aux.Nome};{aux.DataNascimento.ToString("dd/MM/yyyy")};{aux.Relatorio.Temperatura};{aux.Relatorio.Pressao};{aux.Relatorio.Saturacao};{aux.Relatorio.InicioSintomas};{aux.Relatorio.Comorbidades};{aux.Relatorio.QuantComorbidades};{aux.Relatorio.Sintomas};{aux.Relatorio.Exame};{aux.Relatorio.ResultadoExame};");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }


        }
        public void LerArq()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Fábio Moreira\\Arquivados.txt");
                line = sr.ReadLine();
                string[] dados = line.Split(";");

                while (line != null)
                {
                    Entrada(new Paciente(dados[0], dados[1], DateTime.Parse(dados[2]), new Relatorio(dados[3], dados[4], Convert.ToInt32(dados[5]), dados[6], dados[7], Convert.ToInt32(dados[8]), dados[8], dados[9], dados[10])));
                    line = sr.ReadLine();
                    dados = line.Split(";");
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}

