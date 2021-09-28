using System;
using System.IO;
using System.Net;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Produtos
{
    class CriarProduto
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string jsonStr = reader.ReadToEnd();
            CriarProdutoRequest request = CriarProdutoRequest.FromJSON(jsonStr);

            if (request == null || !request.IsValid())
            {
                writer.WriteLine(new BaseResponse() { Message = "Pedido inválido!" }.ToJSON());
                return;
            }

            Categoria cat = Categoria.BuscarID(request.Categoria);
            if (cat == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Categoria não existe!" }.ToJSON());
                return;
            }

            long idcategoria = cat.ID;

            Produto produto = new Produto
            {
                Nome = request.Nome,
                Marca = request.Marca,
                Fabricante = request.Fabricante,
                Ano = request.Ano,
                IDCategoria = idcategoria,
                Descricao = request.Descricao
            };

            if(!produto.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar produto ao BD!" }.ToJSON());
                return;
            }

            CriarProdutoResponse response = new CriarProdutoResponse() {
                Produto = produto,
                Success = true,
                Message = "Produto adicionado com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
