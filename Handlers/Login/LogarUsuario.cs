using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Login
{
    class LogarUsuario
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            LoginRequest request = LoginRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            UsuarioComum usuario = UsuarioComum.BuscarPorLogin(request.Usuario);
            if(usuario == null || usuario.Senha != request.Senha)
            {
                writer.WriteLine(new BaseResponse() { Message = "Combinação usuário e senha não encontrada." }.ToJSON());
                return;
            }

            AcessoUsuario acesso = new AcessoUsuario()
            {
                IDUsuarioComum = usuario.ID,
                Token = Utils.GerarToken()
            };
            if(!acesso.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar token ao banco." }.ToJSON());
                return;
            }

            LogarResponse response = new LogarResponse()
            {
                Success = true,
                Message = "Usuário logado com sucesso.",
                ID = usuario.ID,
                Token = acesso.Token
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
