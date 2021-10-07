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
        public int Pontuacao { get; set; }
        public string Comentario { get; set; }
        public long IDUserComum { get; set; }
        public long IDVenda { get; set; }

        public bool IsValid()
        {
            if (Pontuacao < 0) return false;

            return true;
        }

        public static CriarAvaliacaoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarAvaliacaoRequest>(jsonStr);
        }
    }
}
