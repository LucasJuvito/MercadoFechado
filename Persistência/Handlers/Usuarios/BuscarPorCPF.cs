using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Usuarios
{
    class BuscarPorCPF
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            BuscarUsuarioPorCPFRequest request = BuscarUsuarioPorCPFRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            PessoaFisica pessoa = PessoaFisica.BuscarPeloCPF(request.CPF);
            if (pessoa == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "CPF não encontrado!" }.ToJSON());
                return;
            }

            BuscarUsuarioPorCPFResponse response = new BuscarUsuarioPorCPFResponse() {
                Pessoa = pessoa,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
