using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServidorTestes.Requests
{
    class CriarPessoaFisicaRequest
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime? DataNascimento { get; set; }

        public bool IsValid()
        {
            if (Login == null) return false;
            if (Senha == null) return false;
            if (DataNascimento == null) return false;
            if (CPF == null || CPF.Length != 14) return false;
            if (Nome == null || Nome.Length < 1) return false;

            return true;
        }

        public static CriarPessoaFisicaRequest FromJSON(string jsonStr)
        {
            return JsonConvert.DeserializeObject<CriarPessoaFisicaRequest>(jsonStr);
        }
    }
}
