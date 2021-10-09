using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class BuscarAnuncioPorIDRequest
    {
        public long? ID { get; set; }

        public bool IsValid()
        {
            if (ID == null || ID < 1) return false;

            return true;
        }

        public static BuscarAnuncioPorIDRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<BuscarAnuncioPorIDRequest>(jsonStr);
        }
    }
}
