using System.Collections.Generic;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Comentarios
{
    class ListarComentariosPorAnuncio
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            ListarComentariosPorAnuncioRequest request = ListarComentariosPorAnuncioRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Comentario> comentarios = Comentario.BuscarComentariosPorAnuncio(request.IDAnuncio.Value);
            ListarComentariosPorAnuncioResponse response = new ListarComentariosPorAnuncioResponse() {
                Comentarios = comentarios,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
