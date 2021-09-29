using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarComentarioRequest
    {
        public string Descricao { get; set; }
        public long IDUserComum { get; set; }
        public long IDAnuncio { get; set; }

        public bool IsValid()
        {
            if (Descricao == null) return false;

            return true;
        }

        public static CriarComentarioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarComentarioRequest>(jsonStr);
        }
    }
}
