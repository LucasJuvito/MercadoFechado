using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServidorTestes.Banco;

namespace ServidorTestes.Responses
{
    class BuscarUsuarioPorIDResponse : BaseResponse
    {
        public string Nome { get; set; }
    }
}
