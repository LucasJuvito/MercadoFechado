using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Vendas
{
    class CriarVenda
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarVendaRequest request = CriarVendaRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Venda venda = new Venda
            {
                Data = (DateTime)request.Data,
                IDVendedor = request.Vendedor,
                IDComprador = request.Comprador,
                Valor = request.Valor,
                IDEndereco = request.Endereco,
                IDAnuncio = request.Anuncio
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
