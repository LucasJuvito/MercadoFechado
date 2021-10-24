using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class DeletarAvaliacaoRequest
    {
        public long? ID { get; set; }

        public bool IsValid()
        {
            if (ID == null) return false;

            return true;
        }

        public static DeletarAvaliacaoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<DeletarAvaliacaoRequest>(jsonStr);
        }
    }
}
