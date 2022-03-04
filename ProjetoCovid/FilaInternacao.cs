using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class FilaInternacao
    {
        public Paciente Head { get; set; }
        public Paciente Tail { get; set; }
        int quantidade = 0;
        public FilaInternacao()
        {
            Head = null;
            Tail = null;
        }
        public Paciente Saida()
        {
            Paciente aux = Head;
            if (Head.Proximo == null)
            {
                Tail = null;
            }
            Head = Head.Proximo;
            return aux;
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
        public void ImprimirFilaInternacao()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Fila de Internação");
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
        public void BuscarFilaInternacao()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Fila de internação");
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
                    Console.WriteLine("Nenhum Paciente Encontrado");
                    Console.WriteLine("Pressione uma tecla para voltar ao Menu");
                }
                Console.ReadKey();
            }
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
                if (novoPaciente.Relatorio.QuantComorbidades > Head.Relatorio.QuantComorbidades)
                {
                    novoPaciente.Proximo = Head;
                    Head.Anterior = novoPaciente;
                    Head = novoPaciente;

                }
                else
                {
                    if (novoPaciente.Relatorio.QuantComorbidades < Head.Relatorio.QuantComorbidades)
                    {
                        Tail.Proximo = novoPaciente;
                        novoPaciente.Anterior = Tail;
                        Tail = novoPaciente;
                    }
                    else
                    {
                        Paciente aux1 = Head;
                        do
                        {
                            if (String.Compare(novoPaciente.CPF, aux1.CPF) >= 0)
                            {
                                aux1 = aux1.Proximo;
                            }
                            else
                            {
                                novoPaciente.Proximo = aux1;
                                novoPaciente.Anterior = aux1.Anterior;
                                novoPaciente.Anterior.Proximo = novoPaciente;
                                aux1.Anterior = novoPaciente;
                                aux1 = aux1.Proximo;
                            }
                        } while (aux1 != null);
                    }
                }
                Console.Clear();
            }
            quantidade++;
        }
        void GravarArq(Paciente aux)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\matheus\\FilaInternação.txt", append: true);
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
                StreamReader sr = new StreamReader("C:\\Users\\matheus\\FilaInternação.txt");
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


