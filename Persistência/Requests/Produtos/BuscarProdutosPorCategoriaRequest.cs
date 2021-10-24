using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class BuscarProdutosPorCategoriaRequest
    {
        public long? Categoria { get; set; }

        public bool IsValid()
        {
            if (Categoria == null) return false;

            return true;
        }

        public static BuscarProdutosPorCategoriaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<BuscarProdutosPorCategoriaRequest>(jsonStr);
        }
    }
}
