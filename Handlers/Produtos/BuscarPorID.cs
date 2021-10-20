using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Produtos
{
    class BuscarPorID
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            BuscarProdutoPorIDRequest request = BuscarProdutoPorIDRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Produto produto = Produto.BuscarPorID(request.ID.Value);
            if (produto == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Produto não encontrado!" }.ToJSON());
                return;
            }

            BuscarProdutoPorIDResponse response = new BuscarProdutoPorIDResponse() {
                Produto = produto,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
