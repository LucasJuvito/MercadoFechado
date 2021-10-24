using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Anuncios
{
    class BuscarPorID
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            BuscarAnuncioPorIDRequest request = BuscarAnuncioPorIDRequest.FromJSON(jsonStr);
            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Anuncio anuncio = Anuncio.BuscarPorID(request.ID.Value);
            if (anuncio == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Anúncio não encontrado!" }.ToJSON());
                return;
            }

            BuscarAnuncioPorIDResponse response = new BuscarAnuncioPorIDResponse() {
                Anuncio = anuncio,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
