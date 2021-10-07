using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Anuncios
{
    class AtualizarTitulo
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            AtualizarTituloAnuncioRequest request = AtualizarTituloAnuncioRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            if (!Anuncio.AtualizarTitulo(request.IDAnuncio.Value, request.Titulo))
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível atualizar anúncio no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Anúncio atualizado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
