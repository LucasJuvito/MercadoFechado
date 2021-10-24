using System.Collections.Generic;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Categorias
{
    class ListarCategorias
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            List<Categoria> categorias = Categoria.BuscarTodas();
            ListarCategoriasResponse response = new ListarCategoriasResponse() {
                Categorias = categorias,
                Success = true,
                Message = "Dados obtidos com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
