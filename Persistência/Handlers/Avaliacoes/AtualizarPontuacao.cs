using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Avaliacoes
{
    class AtualizarPontuacao
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            AtualizarPontuacaoAvaliacaoRequest request = AtualizarPontuacaoAvaliacaoRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            if (!Avaliacao.AtualizarPontuacao(request.IDVenda.Value, request.Pontuacao.Value))
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível atualizar avaliação no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Pontuação atualizada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
