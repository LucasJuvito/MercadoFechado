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

namespace ServidorTestes.Handlers.Anuncios
{
    class ListarAnuncios
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            ListarAnunciosPorCategoriaRequest request = ListarAnunciosPorCategoriaRequest.FromJSON(jsonStr);
            if(request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Anuncio> anuncios = Anuncio.ListarPorCategoria(request.IDCategoria.Value);
            ListarAnunciosPorCategoriaResponse response = new ListarAnunciosPorCategoriaResponse()
            {
                Anuncios = anuncios,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
