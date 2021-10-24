using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarProdutoRequest
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Fabricante { get; set; }
        public int Ano { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }

        public bool IsValid()
        {
            if (Nome == null) return false;
            if (Marca == null) return false;
            if (Fabricante == null) return false;
            if (Categoria == null) return false;
            if (Descricao == null) return false;

            return true;
        }

        public static CriarProdutoRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarProdutoRequest>(jsonStr);
        }
    }
}
