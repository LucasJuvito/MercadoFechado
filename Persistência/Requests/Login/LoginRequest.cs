using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class LoginRequest
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            if (Usuario == null || Usuario.Length < 1) return false;
            if (Senha == null || Senha.Length < 1) return false;

            return true;
        }

        public static LoginRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<LoginRequest>(jsonStr);
        }
    }
}
