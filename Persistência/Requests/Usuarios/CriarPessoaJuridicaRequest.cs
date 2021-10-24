using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarPessoaJuridicaRequest
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            if (Login == null || Login.Length < 1) return false;
            if (Senha == null || Senha.Length < 1) return false;
            if (CNPJ == null || CNPJ.Length != 18) return false;
            if (Nome == null || Nome.Length < 1) return false;

            return true;
        }

        public static CriarPessoaJuridicaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarPessoaJuridicaRequest>(jsonStr);
        }
    }
}
