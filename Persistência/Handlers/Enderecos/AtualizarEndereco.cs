using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Handlers.Comentarios
{
    class AtualizarEndereco
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            AtualizarEnderecoRequest request = AtualizarEnderecoRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Endereco endereco = new Endereco()
            {
                CEP = request.CEP,
                SiglaEstado = request.Estado,
                Cidade = request.Cidade
            };

            if (!Endereco.AtualizarEndereco(request.ID.Value, endereco))
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível atualizar comentário no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Comentário atualizado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
