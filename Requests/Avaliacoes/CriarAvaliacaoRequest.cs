using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarAvaliacaoRequest
    {
        public double? Pontuacao { get; set; }
        public string Comentario { get; set; }
        public long? IDVenda { get; set; }

        public bool IsValid()
        {
            if (Pontuacao == null || (Pontuacao.Value % 0.5) != 0) return false;
            if (Comentario == null || Comentario.Length < 1) return false;
            if (IDVenda == null) return false;

            return true;
        }

        public static CriarAvaliacaoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarAvaliacaoRequest>(jsonStr);
        }
    }
}
