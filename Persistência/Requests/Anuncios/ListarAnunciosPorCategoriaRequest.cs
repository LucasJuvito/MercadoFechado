using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Requests
{
    class ListarAnunciosPorCategoriaRequest
    {
        public long? IDCategoria { get; set; }

        public bool IsValid()
        {
            if (IDCategoria == null) return false;

            return true;
        }

        public static ListarAnunciosPorCategoriaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<ListarAnunciosPorCategoriaRequest>(jsonStr);
        }
    }
}
