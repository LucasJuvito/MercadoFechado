using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class BuscarUsuarioPorCPFRequest
    {
        public string CPF { get; set; }

        public bool IsValid()
        {
            if (CPF == null || CPF.Length != 14) return false;

            return true;
        }

        public static BuscarUsuarioPorCPFRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<BuscarUsuarioPorCPFRequest>(jsonStr);
        }
    }
}
