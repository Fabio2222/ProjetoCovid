using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{
    internal class ControleFluxo
    {
        public FilaEntrada FilaEntrada { get; set; }
        public FilaPadrao FilaPadrao { get; set; }
        public FilaPreferencial FilaPreferencial { get; set; }
        public ListaArquivados ListaArquivados { get; set; }
        public CasosCovid FilaCovid { get; set; }
        public FilaInternacao FilaInternacao { get; set; }
        public Internacao Internacao { get; set; }
        int cont = 1;

        //construtor
        public ControleFluxo()
        {
            Internacao = new Internacao();
            FilaInternacao = new FilaInternacao();
            FilaEntrada = new FilaEntrada();
            FilaPadrao = new FilaPadrao();
            FilaPreferencial = new FilaPreferencial();
            ListaArquivados = new ListaArquivados();
            FilaCovid = new CasosCovid();
        }
        public void InserirPaciente() //gerar senha entrada
        {
            string opc = "a";
            Console.WriteLine("Deseja adicionar um novo paciente à fila de entrada");
            Console.WriteLine("[1]SIM [0]NÃO");
            opc = Console.ReadLine();
            if (opc == "1")
            {
                FilaEntrada.Entrada(new Paciente(FilaEntrada.GerarSenha));
                Console.Write("A senha do paciente é : ");
                Console.Write(FilaEntrada.GerarSenha);
                Console.ReadKey();
            }


        }
        public void Recepcao() //2 chamada
        {
            Console.Clear();
            if (FilaEntrada.Vazio())
            {
                Console.WriteLine("Nenhum Paciente na fila");
                Console.WriteLine("Pressione uma tecla para voltar ao Menu");
            }
            else
            {
                int ano1, ano2;
                FilaEntrada.PuxarPaciente();
                Console.WriteLine("Informe o nome do Paciente");
                string nome = Console.ReadLine();
                Console.WriteLine("Informe o CPF");
                string cpf = Console.ReadLine();
                Console.WriteLine("Informe a data de nascimento\nNo padrao dd/mm/aaaa");
                DateTime datanascimento = DateTime.Parse(Console.ReadLine());
                ano1 = datanascimento.Year;
                ano2 = DateTime.Now.Year;
                if ((ano2 - ano1) > 59)
                {
                    FilaPreferencial.Entrada(new Paciente(nome, cpf, datanascimento));
                    Console.WriteLine("Paciente adicionado à fila Preferencial");
                }
                else
                {
                    FilaPadrao.Entrada(new Paciente(nome, cpf, datanascimento));
                    Console.WriteLine("Paciente adicionado à fila padrão");
                }
            }
            Console.ReadKey();
        }

        public void Triagem()
        {
            Console.Clear();
            if (FilaPreferencial.Vazio())
            {
                if (FilaPadrao.Vazio()) // fila vazia
                {
                    Console.WriteLine("Nenhum Paciente na fila para realizar a triagem");
                    Console.WriteLine("Pressione uma tecla para voltar ao Menu");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    CriarRelatorio(FilaPadrao.Saida());  
                    cont = 1;
                }
            }
            else
            {
                if (cont <= 2)  // chamada 2 preferencial e 1 padrao
                {
                    CriarRelatorio(FilaPreferencial.Saida());

                    cont = cont + 1;
                }
                else
                {
                    if (FilaPadrao.Vazio())
                    {
                        Triagem();
                    }
                    else
                    {
                        CriarRelatorio(FilaPadrao.Saida());
                    }
                    cont = 1;
                }
            }
            Console.ReadKey();
        }
              // relatorio paciente
        public void CriarRelatorio(Paciente paciente)
        {
            string exameteste = "a";
            string exame = "a";
            string resultadoExame = "a";
            string opc = "a";
            bool selec = false;
            Console.WriteLine(paciente.ToString());
            Console.Write("Temperatura: ");
            string temp = Console.ReadLine();
            Console.Write("Pressão: ");
            string pressao = Console.ReadLine();
            Console.Write("Saturação: ");
            int sat = Convert.ToInt32(Console.ReadLine());
            Console.Write("Possui sintomas? Se sim quais? ");
            string sintomas = Console.ReadLine();
            Console.Write("Se sim, Quantos dias de Sintomas? ");
            string sint = Console.ReadLine();
            Console.Write("Possui Comorbidades?, Informar (0,1,2,3...): ");
            int quantComor = Convert.ToInt32(Console.ReadLine());
            Console.Write("Se sim, quais comorbidades? ");
            string comorbidades = Console.ReadLine();
            Console.Write("Solicitar Exame Covid?\n[1]SIM [0]NÃO");
            string opc1 = Console.ReadLine();
            if (opc1 == "1")
            {
                Console.WriteLine("Qual resultado do exame?\n[1]Negativo [2]Positivo: ");
                exame = "SIM";
                exameteste = Console.ReadLine();
            }
            else
            {
                exame = ("NÃO");
                resultadoExame = ("Exame não solicitado");

            }
            if (sat < 90)
            {
                Internacao.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                Console.WriteLine("Paciente adicionado à interna~çao devido a saturação baixa");
            }
            else
            {
                if (exameteste == "1")
                {
                    resultadoExame = ("Negativo");
                    ListaArquivados.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                    Console.WriteLine("Paciente Arquivado. Teste covid negativo");
                    return;
                }
                if (exameteste == "2")
                {
                    resultadoExame = ("Positivo");
                }
                do
                {

                    Console.Write("[1]Internação");
                    Console.Write(" [2]Acompanhar Caso");
                    if (resultadoExame != "Positivo")
                    {
                        Console.Write(" [3]Arquivar paciente ");
                    }
                    opc = Console.ReadLine();
                    if (opc == "3")
                    {

                        ListaArquivados.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                        Console.WriteLine("Paciente Arquivado");

                        selec = true;
                    }
                    else if (opc == "2")
                    {
                        FilaCovid.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                        Console.WriteLine("Paciente adicionado à Lista de acompanhamentos");
                        selec = true;
                    }
                    else if (opc == "1")
                    {
                        if (Internacao.Cheio())
                        {
                            Console.WriteLine("Internação lotada!!");
                            Console.WriteLine("Paciente adicionado à fila de internação!!");
                            FilaInternacao.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                        }
                        else
                        {
                            Internacao.Entrada(new Paciente(paciente, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                            Console.WriteLine("Paciente adicionado á internação");
                        }
                        selec = true;
                    }
                    else
                    {
                        Console.WriteLine("Favor selecionar uma das opções disponíveis");
                    }

                } while (selec != true);

            }
            Console.ReadKey();
        }

        public void InserirInternacao()
        {
            string exame = "0";
            string resultadoExame = "0";
            if (Internacao.Cheio())
            {
                Console.WriteLine("Leitos nao disponíveis, paciente será incluso na fila de Internação");
            }
            Console.WriteLine("Informe o nome do Paciente");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o CPF");
            string cpf = Console.ReadLine();
            Console.WriteLine("Informe a data de nascimento\nNo padrão dd/mm/aaaa");
            DateTime datanascimento = DateTime.Parse(Console.ReadLine());
            Console.Write("Temperatura: ");
            string temp = Console.ReadLine();
            Console.Write("Pressão: ");
            string pressao = Console.ReadLine();
            Console.Write("Saturação: ");
            int sat = Convert.ToInt32(Console.ReadLine());
            Console.Write("Possui sintomas? Se sim quais? ");
            string sintomas = Console.ReadLine();
            Console.Write("Se sim, Dias com Sintomas ");
            string sint = Console.ReadLine();
            Console.Write("Possui Comorbidades?, Informar (0,1,2,3...): ");
            int quantComor = Convert.ToInt32(Console.ReadLine());
            Console.Write("Se sim, quais comorbidades? ");
            string comorbidades = Console.ReadLine();
            Console.Write("Solicitar Exame Covid?\n[1]SIM [0]NÃO : ");
            string opc = Console.ReadLine();
            if (opc == "1")
            {
                Console.WriteLine("Qual resultado do exame?");
                exame = "SIM";
                resultadoExame = Console.ReadLine();
            }
            else
            {
                exame = ("NÃO");
                resultadoExame = ("Exame não solicitado");

            }
            if (Internacao.Cheio())
            {
                FilaInternacao.Entrada(new Paciente(cpf, nome, datanascimento, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                Console.WriteLine("Paciente adicionado à fila de internação!!");
            }
            else
            {
                Internacao.Entrada(new Paciente(cpf, nome, datanascimento, new Relatorio(temp, pressao, sat, sint, comorbidades, quantComor, sintomas, exame, resultadoExame)));
                Console.WriteLine("Paciente adicionado à internação");
            }
            Console.ReadLine();
        }

        public void BuscarInternacaoAlta()
        {
            Console.Clear();

            if (Internacao.Vazio())
            {
                Console.WriteLine("Nenhum Paciente na Internação");
                Console.WriteLine("Pressione uma tecla para voltar ao Menu");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Informe o CPF do paciente que deseja buscar");
                string busca = Console.ReadLine();
                Paciente aux = Internacao.Head;
                bool cont = false;
                do
                {
                    if (aux.CPF == (busca))
                    {
                        AltaInternacao(aux);

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
        public void AltaInternacao(Paciente paciente)
        {

            if (paciente.CPF == Internacao.Head.CPF)
            {
                Console.WriteLine(paciente.ToString());
                Console.WriteLine(paciente.Relatorio.ToString());
                ListaArquivados.Entrada(paciente);
                Console.WriteLine("Alta! Ficha do paciente arquivada");
                Internacao.Head = Internacao.Head.Proximo;
            }
            else
            {
                Paciente aux = Internacao.Head;
                Paciente aux1 = Internacao.Head.Proximo;

                for (int i = 1; i < Internacao.Quantidade; i++)
                {
                    if (string.Compare(paciente.Nome, aux1.Nome) == 0)
                    {
                        Console.WriteLine(paciente.ToString());
                        Console.WriteLine(paciente.Relatorio.ToString());
                        ListaArquivados.Entrada(paciente);
                        Console.WriteLine("Alta!. Ficha paciente arquivada");

                        aux.Proximo = aux1.Proximo;
                        if (aux1.Proximo == null)
                        {
                            Internacao.Tail = aux;
                        }
                    }
                    else
                    {
                        aux = aux1;
                        aux1 = aux1.Proximo;

                    }

                }
            }

            if (Internacao.Head == null)
            {
                Internacao.Tail = null;
            }
            Internacao.Quantidade--;
            Console.ReadKey();

            if (Internacao.Cheio())
            {

            }
            else
            {
                if (FilaInternacao.Vazio())
                {

                }
                else
                {
                    Internacao.Entrada(FilaInternacao.Saida());
                }
            }
        }
    }
}

