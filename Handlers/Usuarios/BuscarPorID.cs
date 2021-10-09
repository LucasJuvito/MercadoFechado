using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Usuarios
{
    class BuscarPorID
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            BuscarUsuarioPorIDRequest request = BuscarUsuarioPorIDRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            PessoaFisica pessoa = PessoaFisica.BuscarPeloID(request.ID.Value);
            if (pessoa != null)
            {
                BuscarUsuarioPorIDResponse response = new BuscarUsuarioPorIDResponse()
                {
                    Nome = pessoa.Nome,
                    Success = true,
                    Message = "Dados obtidos com sucesso!"
                };
                writer.WriteLine(response.ToJSON());
                return;
            }

            PessoaJuridica pessoaJuridica = PessoaJuridica.BuscarPeloID(request.ID.Value);
            if (pessoaJuridica != null)
            {
                BuscarUsuarioPorIDResponse response = new BuscarUsuarioPorIDResponse()
                {
                    Nome = pessoaJuridica.NomeFantasia,
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
