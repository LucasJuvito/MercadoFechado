using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Vendas
{
    class EfetuarCompra
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            string token = context.Request.Headers.Get("Authorization");

            EfetuarCompraRequest request = EfetuarCompraRequest.FromJSON(jsonStr);
            if (token == null || request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(token);
            if(usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            Anuncio anuncio = Anuncio.BuscarPorID(request.IDAnuncio.Value);
            if(anuncio == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Anúncio não encontrado!" }.ToJSON());
                return;
            }

            Endereco endereco = Endereco.BuscarPorID(request.IDEndereco.Value);
            if(endereco == null || endereco.IDProprietario != usuarioLogado.IDUsuarioComum)
            {
                writer.WriteLine(new BaseResponse() { Message = "Endereço não encontrado!" }.ToJSON());
                return;
            }

            Venda venda = new Venda
            {
                Data = DateTime.Now,
                IDVendedor = anuncio.IDVendedor,
                IDComprador = usuarioLogado.IDUsuarioComum,
                Valor = anuncio.Valor,
                IDEndereco = request.IDEndereco.Value,
                IDAnuncio = anuncio.ID
            };

            if(!venda.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar venda ao BD!" }.ToJSON());
                return;
            }

            CriarVendaResponse response = new CriarVendaResponse() {
                Venda = venda,
                Success = true,
                Message = "Venda adicionado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
