using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Usuarios
{
    class CriarPessoaFisica
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarPessoaFisicaRequest request = CriarPessoaFisicaRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            if (UsuarioComum.BuscarPorLogin(request.Login) != null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Login já existe!" }.ToJSON());
                return;
            }

            if(!UsuarioComum.CriarPessoaFisica(request.Login, request.Senha, request.CPF, request.Nome, request.DataNascimento))
            {
                writer.WriteLine(new BaseResponse() { Message = "Falha ao criar usuário no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse() {
                Success = true,
                Message = "Conta criada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
