using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Avaliacoes
{
    class CriarAvaliacao
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarAvaliacaoRequest request = CriarAvaliacaoRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Avaliacao avaliacao = new Avaliacao
            {
                Pontuacao = request.Pontuacao,
                Comentario = request.Comentario,
                IDUserComum = request.IDUserComum,
                IDVenda = request.IDVenda
            };

            if(!avaliacao.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar avaliação ao BD!" }.ToJSON());
                return;
            }

            CriarAvaliacaoResponse response = new CriarAvaliacaoResponse() {
                Avaliacao = avaliacao,
                Success = true,
                Message = "Avaliação adicionada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
