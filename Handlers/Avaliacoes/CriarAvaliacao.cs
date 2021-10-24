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
            string token = context.Request.Headers.Get("Authorization");
            
            CriarAvaliacaoRequest request = CriarAvaliacaoRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(token);
            if (usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            Venda venda = Venda.BuscarPorID(request.IDVenda.Value);
            if(venda == null || venda.IDComprador != usuarioLogado.IDUsuarioComum)
            {
                writer.WriteLine(new BaseResponse() { Message = "Venda não encontrada!" }.ToJSON());
                return;
            }

            Avaliacao avaliacao = new Avaliacao
            {
                IDVenda = request.IDVenda.Value,
                Pontuacao = request.Pontuacao.Value,
                Comentario = request.Comentario
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
