using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class ListarComentariosPorAnuncioRequest
    {
        public long? IDAnuncio { get; set; }

        public bool IsValid()
        {
            if (IDAnuncio == null) return false;

            return true;
        }

        public static ListarComentariosPorAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<ListarComentariosPorAnuncioRequest>(jsonStr);
        }
    }
}
