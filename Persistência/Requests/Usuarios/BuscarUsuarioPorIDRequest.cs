using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class BuscarUsuarioPorIDRequest
    {
        public long? ID { get; set; }

        public bool IsValid()
        {
            if (ID == null) return false;

            return true;
        }

        public static BuscarUsuarioPorIDRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<BuscarUsuarioPorIDRequest>(jsonStr);
        }
    }
}
