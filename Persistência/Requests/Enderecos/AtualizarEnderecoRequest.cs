using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class AtualizarEnderecoRequest
    {
        public long? ID { get; set; }
        public string CEP { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Quadra { get; set; }
        public string Complemento { get; set; }
        public long? IDProprietario { get; set; }

        public bool IsValid()
        {
            if (ID == null) return false;
            if (CEP == null) return false;
            if (Numero == null) return false;
            if (Bairro == null) return false;
            if (Cidade == null) return false;
            if (Estado == null) return false;
            if (Quadra == null) return false;
            if (IDProprietario == null) return false;

            return true;
        }

        public static AtualizarEnderecoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<AtualizarEnderecoRequest>(jsonStr);
        }
    }
}
