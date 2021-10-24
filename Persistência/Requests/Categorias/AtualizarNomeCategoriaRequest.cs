using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarNomeCategoriaRequest
    {
        public long? ID { get; set; }
        public string Nome { get; set; }

        public bool IsValid()
        {
            if (Nome == null) return false;

            return true;
        }

        public static AtualizarNomeCategoriaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarNomeCategoriaRequest>(jsonStr);
        }
    }
}
