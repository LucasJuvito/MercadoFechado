using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Handlers.Enderecos
{
    class ListarEstados
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            List<Estado> estados = Estado.ListarTodos();
            ListarEstadosResponse response = new ListarEstadosResponse()
            {
                Estados = estados,
                Success = true,
                Message = "Vendas obtidas com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
