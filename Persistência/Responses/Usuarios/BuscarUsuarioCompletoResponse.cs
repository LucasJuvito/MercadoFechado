using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServidorTestes.Banco;

namespace ServidorTestes.Responses
{
    class BuscarUsuarioCompletoResponse : BaseResponse
    {
        public string Nome { get; set; }
        public int TipoPessoa { get; set; }
        public string Identificador { get; set; }
    }
}
