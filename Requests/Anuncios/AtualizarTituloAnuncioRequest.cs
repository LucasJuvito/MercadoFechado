using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarTituloAnuncioRequest
    {
        public Int64? IDAnuncio { get; set; }
        public string Titulo { get; set; }

        public bool IsValid()
        {
            if (Titulo == null) return false;
            if (IDAnuncio == null) return false;

            return true;
        }

        public static AtualizarTituloAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarTituloAnuncioRequest>(jsonStr);
        }
    }
}
