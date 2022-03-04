using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class FilaEntrada
    {
        public Paciente Head { get; set; }
        public Paciente Tail { get; set; }
        public int GerarSenha { get; set; }


        public FilaEntrada()
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
            novoPaciente.Senha = GerarSenha;
            GerarSenha++;
        }
        public void ImprimirFilaEntrada()
        {

            if (Vazio())
            {
                Console.WriteLine("Fila de entrada vazia");
            }
            else
            {
                Paciente aux = Head;
                do
                {
                    Console.WriteLine(aux.Senha);
                    aux = aux.Proximo;
                } while (aux != null);
            }
        }
        public void PuxarPaciente()
        {
            if (Vazio())
            {
                Console.WriteLine("Fila de entrada vazia");
                return;
            }
            else
            {

                Console.WriteLine("Senha: {0}", Head.Senha + 1);

                if (Head.Proximo == null)
                {
                    Tail = null;
                }
                Head = Head.Proximo;

            }

        }
        public void Saida(int senha)
        {

            Paciente aux = Head;
            Paciente aux1 = Head.Proximo;
            if (aux.Senha == senha)
            {
                Head = Head.Proximo;
            }
            else
            {

                for (int i = 0; i < GerarSenha; i++)
                {
                    if (senha == aux1.Senha)
                    {
                        aux.Proximo = aux1.Proximo;
                        if (aux1.Proximo == null)
                        {
                            Tail = aux;
                        }
                    }
                    else
                    {
                        aux = aux1;
                        aux1 = aux1.Proximo;
                    }
                }
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
    }
}

