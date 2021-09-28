using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Enderecos
{
    class CriarEndereco
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarEnderecoRequest request = CriarEnderecoRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Endereco endereco = new Endereco
            {
                CEP = request.CEP,
                Estado = request.Estado,
                Cidade = request.Cidade,
                Bairro = request.Bairro,
                Quadra = request.Quadra,
                Numero = request.Numero,
                Complemento = request.Complemento,
                IDProprietario = request.IDProprietario
            };

            if(!endereco.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar endereço ao BD!" }.ToJSON());
                return;
            }

            CriarEnderecoResponse response = new CriarEnderecoResponse() {
                Endereco = endereco,
                Success = true,
                Message = "endereco adicionado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
