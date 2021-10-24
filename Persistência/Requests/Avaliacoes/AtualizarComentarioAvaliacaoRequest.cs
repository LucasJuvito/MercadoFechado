using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarComentarioAvaliacaoRequest
    {
        public Int64? IDVenda { get; set; }
        public string Comentario { get; set; }

        public bool IsValid()
        {
            if (IDVenda == null) return false;
            if (Comentario == null) return false;

            return true;
        }

        public static AtualizarComentarioAvaliacaoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarComentarioAvaliacaoRequest>(jsonStr);
        }
    }
}
