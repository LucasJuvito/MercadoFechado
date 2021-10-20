using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarAnuncioRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public long? IDProduto { get; set; }
        public long? IDVendedor { get; set; }
        public double? Valor { get; set; }

        public bool IsValid()
        {
            if (Titulo == null) return false;
            if (Descricao == null) return false;
            if (IDProduto == null) return false;
            if (IDVendedor == null) return false;
            if (Valor == null) return false;

            return true;
        }

        public static CriarAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarAnuncioRequest>(jsonStr);
        }
    }
}
