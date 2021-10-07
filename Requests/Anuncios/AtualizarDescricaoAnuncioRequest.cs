using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarDescricaoAnuncioRequest
    {
        public Int64? IDAnuncio { get; set; }
        public string Descricao { get; set; }

        public bool IsValid()
        {
            if (Descricao == null) return false;
            if (IDAnuncio == null) return false;

            return true;
        }

        public static AtualizarDescricaoAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarDescricaoAnuncioRequest>(jsonStr);
        }
    }
}
