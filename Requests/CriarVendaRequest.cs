using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarVendaRequest
    {
        public DateTime? Data { get; set; }
        public long Vendedor { get; set; }
        public long Comprador { get; set; }
        public int Valor { get; set; }
        public long Endereco { get; set; }
        public long Anuncio { get; set; }

        public bool IsValid()
        {
            if (Valor < 0) return false;

            return true;
        }

        public static CriarVendaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarVendaRequest>(jsonStr);
        }
    }
}
