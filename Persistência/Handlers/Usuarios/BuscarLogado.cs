using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Usuarios
{
    class BuscarLogado
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

            UsuarioComum usuarioComum = UsuarioComum.BuscarPorID(usuarioLogado.IDUsuarioComum);
            if (usuarioComum == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não encontrado!" }.ToJSON());
                return;
            }

            PessoaFisica pessoa = PessoaFisica.BuscarPeloID(usuarioLogado.IDUsuarioComum);
            if (pessoa != null)
            {
                BuscarUsuarioCompletoResponse response = new BuscarUsuarioCompletoResponse()
                {
                    Login = usuarioComum.Login,
                    Saldo = usuarioComum.Saldo,
                    Nome = pessoa.Nome,
                    TipoPessoa = 1,
                    Identificador = pessoa.CPF,
                    Success = true,
                    Message = "Dados obtidos com sucesso!"
                };
                writer.WriteLine(response.ToJSON());
                return;
            }

            PessoaJuridica pessoaJuridica = PessoaJuridica.BuscarPeloID(usuarioLogado.IDUsuarioComum);
            if (pessoaJuridica != null)
            {
                BuscarUsuarioCompletoResponse response = new BuscarUsuarioCompletoResponse()
                {
                    Login = usuarioComum.Login,
                    Saldo = usuarioComum.Saldo,
                    Nome = pessoaJuridica.NomeFantasia,
                    TipoPessoa = 2,
                    Identificador = pessoaJuridica.CNPJ,
                    Success = true,
                    Message = "Dados obtidos com sucesso!"
                };
                writer.WriteLine(response.ToJSON());
                return;
            }

            writer.WriteLine(new BaseResponse() { Message = "Não foi possível encontrar o usuário no BD!" }.ToJSON());
            return;
        }
    }
}
