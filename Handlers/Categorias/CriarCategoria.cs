using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Categorias
{
    class CriarCategoria
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarCategoriaRequest request = CriarCategoriaRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Categoria categoria = new Categoria
            {
                Nome = request.Nome
            };

            if(!categoria.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar categoria ao BD!" }.ToJSON());
                return;
            }

            CriarCategoriaResponse response = new CriarCategoriaResponse() {
                Categoria = categoria,
                Success = true,
                Message = "Categoria adicionada com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
