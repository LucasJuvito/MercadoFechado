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

namespace ServidorTestes.Handlers.Avaliacoes
{
    class DeletarAvaliacao
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            string token = context.Request.Headers.Get("Authorization");

            DeletarAvaliacaoRequest request = DeletarAvaliacaoRequest.FromJSON(jsonStr);
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

            Avaliacao avaliacao = Avaliacao.BuscarPorID(request.ID.Value);
            if (avaliacao == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível encontrar avaliação no BD!" }.ToJSON());
                return;
            }

            Venda venda = Venda.BuscarPorID(avaliacao.IDVenda);
            if(venda == null || venda.IDComprador != usuarioLogado.IDUsuarioComum)
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível encontrar avaliação no BD!" }.ToJSON());
                return;
            }

            if(!avaliacao.DeletarDoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível deletar avaliação no BD!" }.ToJSON());
                return;
            }

            BaseResponse response = new BaseResponse()
            {
                Success = true,
                Message = "Avaliação deletado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
