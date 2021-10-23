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

namespace ServidorTestes.Handlers.Comentarios
{
    class DeletarComentario
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            string token = context.Request.Headers.Get("Authorization");

            DeletarComentarioAnuncioRequest request = DeletarComentarioAnuncioRequest.FromJSON(jsonStr);
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

            Comentario comentario = Comentario.BuscarPorID(request.ID.Value);
            if (comentario == null || comentario.IDUserComum != usuarioLogado.IDUsuarioComum)
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível encontrar comentário no BD!" }.ToJSON());
                return;
            }

            if(!comentario.DeletarDoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível deletar comentário do BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Comentário deletado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
