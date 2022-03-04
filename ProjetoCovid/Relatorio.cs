using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCovid
{

    internal class Relatorio
    {
        public string Temperatura { get; set; }
        public string Pressao { get; set; }
        public int Saturacao { get; set; }
        public string InicioSintomas { get; set; }
        public string Comorbidades { get; set; }
        public int QuantComorbidades { get; set; }
        public string Sintomas { get; set; }
        public string Exame { get; set; }
        public string ResultadoExame { get; set; }
        public Relatorio(string temperatura, string pressao, int saturacao, string inicioSintomas, string comorbidades, int quantComorbidades, string sintomas, string exame, string resultadoExame)
        {
            Temperatura = temperatura;
            Pressao = pressao;
            Saturacao = saturacao;
            InicioSintomas = inicioSintomas;
            Comorbidades = comorbidades;
            QuantComorbidades = quantComorbidades;
            Sintomas = sintomas;
            Exame = exame;
            ResultadoExame = resultadoExame;

        }
        public override string ToString()
        {
            return "Temperatura:\t" + Temperatura + "\nPressao:\t" + Pressao + "\nSaturacao:\t" + Saturacao + "\nSintomas:\t" + Sintomas + "\nDias de Sintomas:\t" + InicioSintomas + "\n" + QuantComorbidades + " :Comorbidades:\t" + Comorbidades + "\nExame Covid:\t" + ResultadoExame;
        }
    }
}
