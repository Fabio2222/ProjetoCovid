using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class FilaCovid
    {
        public Paciente Head { get; set; }
        public Paciente Tail { get; set; }
        public FilaCovid()
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
        public void ImprimirFilaCovid()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Fila de Covid");
            }
            else
            {
                Paciente aux = Head;
                do
                {
                    Console.WriteLine(aux);
                    Console.WriteLine(aux.Relatorio.ToString());
                    Console.ReadKey();
                    aux = aux.Proximo;
                } while (aux != null);
            }
            Console.ReadKey();
        }
        public void BuscarFilaCovid()
        {
            Console.Clear();
            if (Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Fila de Covid");
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
                }
                Console.ReadKey();
            }
        }
    }
}

