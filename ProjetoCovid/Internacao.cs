using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class Internacao
    {
        public Paciente Head { get; set; }
        public Paciente Tail { get; set; }

        public int Quantidade { get; set; }
        int leitosTotais = 0;
        public Internacao()
        {
            Head = null;
            Tail = null;
            Quantidade = 0;
        }
        public bool Vazio()  //leitos vazios
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

        public void Leitos(int quant) //coloca a quantidade de leitos na abertura do programa
        {
            leitosTotais = quant;
        }
        public void Leitos()
        {
            Console.WriteLine("Quantos leitos tem no hospital");  //alterar quantidade
            int leitos = Convert.ToInt32(Console.ReadLine());
            if (Contador() > leitos)
            {
                Console.WriteLine("O hospital ja possui {0} leitos ocupados\nVerificar e informar a quantidade de leitos correta.", Contador());
            }
            else
            {
                Console.WriteLine("Quantidade de Leitos Atualizada");
                leitosTotais = leitos;
            }
            Console.ReadKey();
        }
        int Contador()  // cona a quant de leitos
        {
            int contador = 0;
            if (Vazio())
            {
                contador = 0;
            }
            else
            {
                Paciente aux = Head;
                do
                {
                    aux = aux.Proximo;
                    contador++;
                } while (aux != null);
            }
            return contador; //retorna um valor inteiro
        }

        public void VerificarLeitos()  //verifica leitos disponíveis
        {
            if (leitosTotais >= Contador())
            {
                Console.WriteLine("Ha um total de {0} leitos disponíveis", leitosTotais - Contador());
            }
            else
            {
                Console.WriteLine("Não há leitos disponíveis no momento");
                //Console.WriteLine("Paciente foi adicionado a Fila de espera de Internacao?");

            }
            Console.ReadKey();
        }
        public void Entrada(Paciente novoPaciente)
        {
            if (Vazio())  // entrada em fila
            {
                Head = novoPaciente;
                Tail = novoPaciente;

            }
            else
            {
                Tail.Proximo = novoPaciente;
                Tail = novoPaciente;

            }
            GravarArq(novoPaciente);  // grava o arquivo
            Quantidade++;
        }

        public bool Cheio()  //leitos cheios
        {
            if (leitosTotais == Contador())
            { return true; }
            else
            {
                return false;
            }
        }
        public void BuscarInternacao()  //busca paciente internado
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na  Internação");
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
                    if (aux.CPF == (busca))
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


        public void ImprimirInternacao()  //imprime pacientes na internacao
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Internação");
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

        void GravarArq(Paciente aux)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Fábio Moreira\\Internacao.txt", append: true);
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
                StreamReader sr = new StreamReader("C:\\Users\\Fábio Moreira\\Internacao.txt");
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

