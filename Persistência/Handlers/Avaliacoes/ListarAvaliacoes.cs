using ServidorTestes.Banco;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Handlers.Avaliacoes
{
    class ListarAvaliacoes
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            ListarAvaliacoesPorAnuncioRequest request = ListarAvaliacoesPorAnuncioRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Avaliacao> avaliacoes = Avaliacao.ListarPorAnuncio(request.IDAnuncio.Value);
            ListarAvaliacoesPorAnuncioResponse response = new ListarAvaliacoesPorAnuncioResponse()
            {
                Avaliacoes = avaliacoes,
                Success = true,
                Message = "Avaliações obtidas com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
