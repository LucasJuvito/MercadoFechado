using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarEnderecoRequest
    {
        public string CEP { get; set; }
        public int? Numero { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Quadra { get; set; }
        public string Complemento { get; set; }

        public bool IsValid()
        {
            if (CEP == null) return false;
            if (Numero == null) return false;
            if (Estado == null) return false;
            if (Cidade == null) return false;
            if (Quadra == null) return false;

            return true;
        }

        public static CriarEnderecoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarEnderecoRequest>(jsonStr);
        }
    }
}
