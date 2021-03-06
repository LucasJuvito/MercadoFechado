using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Comentarios
{
    class CriarComentario
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            string token = context.Request.Headers.Get("Authorization");

            CriarComentarioRequest request = CriarComentarioRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(token);
            if (usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            Comentario comentario = new Comentario
            {
                Descricao = request.Descricao,
                IDUserComum = usuarioLogado.IDUsuarioComum,
                IDAnuncio = request.IDAnuncio.Value
            };

            if(!comentario.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar comentário ao BD!" }.ToJSON());
                return;
            }

            CriarComentarioResponse response = new CriarComentarioResponse() {
                Comentario = comentario,
                Success = true,
                Message = "Comentário adicionado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
