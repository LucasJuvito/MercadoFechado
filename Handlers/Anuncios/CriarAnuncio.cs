using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Anuncios
{
    class CriarAnuncio
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarAnuncioRequest request = CriarAnuncioRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Anuncio anuncio = new Anuncio
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                IDProduto = request.IDProduto.Value,
                IDVendedor = request.IDVendedor.Value,
                Valor = request.Valor.Value
            };

            if(!anuncio.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar anúncio ao BD!" }.ToJSON());
                return;
            }

            CriarAnuncioResponse response = new CriarAnuncioResponse() {
                Anuncio = anuncio,
                Success = true,
                Message = "Anúncio adicionado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
