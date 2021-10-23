using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Login
{
    class DeslogarUsuario
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string token = context.Request.Headers.Get("Authorization");
            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(token);
            if (usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            if (!usuarioLogado.RemoverDoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível destruir token no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Usuário deslogado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
