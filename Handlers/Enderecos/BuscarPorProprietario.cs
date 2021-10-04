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
            string jsonStr = reader.ReadToEnd();
            BuscarEnderecosPorProprietarioRequest request = BuscarEnderecosPorProprietarioRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Endereco> enderecos = Endereco.BuscarPorProprietario(request.ID.Value);

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
