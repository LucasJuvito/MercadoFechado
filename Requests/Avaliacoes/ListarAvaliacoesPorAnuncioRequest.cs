using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServidorTestes.Banco;

namespace ServidorTestes.Responses
{
    class ListarAvaliacoesPorAnuncioRequest : BaseResponse
    {
        public long? IDAnuncio { get; set; }

        public bool IsValid()
        {
            if (IDAnuncio == null) return false;

            return true;
        }

        public static ListarAvaliacoesPorAnuncioRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<ListarAvaliacoesPorAnuncioRequest>(jsonStr);
        }
    }
}
