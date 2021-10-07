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

namespace ServidorTestes.Handlers.Vendas
{
    class ListarVendasPorAnuncio
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            ListarVendasPorAnuncioRequest request = ListarVendasPorAnuncioRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Venda> vendas = Venda.ListarVendas(request.IDAnuncio.Value);
            ListarVendasPorAnuncioResponse response = new ListarVendasPorAnuncioResponse()
            {
                Vendas = vendas,
                Success = true,
                Message = "Avaliações obtidas com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
