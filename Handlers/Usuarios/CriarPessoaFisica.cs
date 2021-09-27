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

            UsuarioComum usuario = new UsuarioComum
            {
                Login = request.Login,
                Senha = request.Senha
            };

            if(!usuario.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar usuário ao BD!" }.ToJSON());
                return;
            }

            PessoaFisica pessoa = new PessoaFisica
            {
                ID = usuario.ID,
                CPF = request.CPF,
                Nome = request.Nome,
                DataNascimento = (DateTime)request.DataNascimento
            };

            if(!pessoa.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar pessoa física ao BD!" }.ToJSON());
                return;
            }

            CriarPessoaFisicaResponse response = new CriarPessoaFisicaResponse() {
                Usuario = usuario,
                Pessoa = pessoa,
                Success = true,
                Message = "Conta criada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
