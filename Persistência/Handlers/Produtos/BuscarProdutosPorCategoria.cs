using System.Collections.Generic;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Produtos
{
    class BuscarProdutosPorCategoria
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            BuscarProdutosPorCategoriaRequest request = BuscarProdutosPorCategoriaRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            List<Produto> produtos = Produto.BuscarPorCategoria(request.Categoria.Value);

            BuscarProdutosPorCategoriaResponse response = new BuscarProdutosPorCategoriaResponse()
            {
                Produtos = produtos,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
