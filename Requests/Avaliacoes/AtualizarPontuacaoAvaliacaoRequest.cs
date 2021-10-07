using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarPontuacaoAvaliacaoRequest
    {
        public Int64? IDVenda { get; set; }
        public int? Pontuacao { get; set; }

        public bool IsValid()
        {
            if (Pontuacao == null) return false;
            if (IDVenda == null) return false;

            return true;
        }

        public static AtualizarPontuacaoAvaliacaoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarPontuacaoAvaliacaoRequest>(jsonStr);
        }
    }
}
