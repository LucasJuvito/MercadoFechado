using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class BuscarProdutoPorIDRequest
    {
        public long? ID { get; set; }

        public bool IsValid()
        {
            if (ID == null || ID < 1) return false;

            return true;
        }

        public static BuscarProdutoPorIDRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<BuscarProdutoPorIDRequest>(jsonStr);
        }
    }
}
