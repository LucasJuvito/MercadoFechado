using System.Collections.Generic;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Enderecos
{
    class BuscarPorProprietario
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

            List<Endereco> enderecos = Endereco.BuscarPorProprietario(usuarioLogado.IDUsuarioComum);

            BuscarEnderecosPorProprietarioResponse response = new BuscarEnderecosPorProprietarioResponse()
            {
                Enderecos = enderecos,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
