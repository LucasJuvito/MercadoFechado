using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarComentarioAnuncioRequest
    {
        public long? ID { get; set; }
        public string Descricao { get; set; }

        public bool IsValid()
        {
            if (ID == null) return false;
            if (Descricao == null) return false;

            return true;
        }

        public static AtualizarComentarioAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarComentarioAnuncioRequest>(jsonStr);
        }
    }
}
