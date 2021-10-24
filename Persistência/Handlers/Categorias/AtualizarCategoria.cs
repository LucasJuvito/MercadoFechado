using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Categorias
{
    class AtualizarCategoria
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            AtualizarNomeCategoriaRequest request = AtualizarNomeCategoriaRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            if (!Categoria.AtualizarNome(request.ID.Value, request.Nome))
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível atualizar categoria no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Categoria atualizada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
