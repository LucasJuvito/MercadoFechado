using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarCategoriaRequest
    {
        public string Nome { get; set; }

        public bool IsValid()
        {
            if (Nome == null) return false;

            return true;
        }

        public static CriarCategoriaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarCategoriaRequest>(jsonStr);
        }
    }
}
