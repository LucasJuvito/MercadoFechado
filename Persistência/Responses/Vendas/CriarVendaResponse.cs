using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServidorTestes.Banco;

namespace ServidorTestes.Responses
{
    class CriarVendaResponse : BaseResponse
    {
        public Venda Venda { get; set; }
    }
}
