using ServidorTestes.Banco;
using System;
using System.IO;
using System.Net;

namespace ServidorTestes.Handlers.Produtos
{
    class BuscarFotoProduto
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string str = context.Request.QueryString.Get("id_produto");
            Produto produto = Produto.BuscarPorIDComImagem(Convert.ToInt64(str));

            writer.BaseStream.Write(produto.Imagem);
        }
    }
}
