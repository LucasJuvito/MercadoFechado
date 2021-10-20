using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class EfetuarCompraRequest
    {
        public long? IDAnuncio { get; set; }
        public long? IDEndereco { get; set; }

        public bool IsValid()
        {
            if (IDAnuncio == null || IDAnuncio.Value < 1) return false;

            return true;
        }

        public static EfetuarCompraRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<EfetuarCompraRequest>(jsonStr);
        }
    }
}
