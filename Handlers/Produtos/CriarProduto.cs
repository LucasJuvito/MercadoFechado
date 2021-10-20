using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using ServidorTestes.Banco;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Produtos
{
    class CriarProduto
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            var data = context.Request.InputStream;
            var streamContent = new StreamContent(data);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(context.Request.ContentType);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var provider = streamContent.ReadAsMultipartAsync().Result;
            MemoryStream imagem = new MemoryStream();

            foreach (var httpContent in provider.Contents)
            {
                var fileName = httpContent.Headers.ContentDisposition.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    dict.Add(httpContent.Headers.ContentDisposition.Name.Replace("\"", ""), httpContent.ReadAsStringAsync().Result.Replace("\"", ""));
                    continue;
                }

                using (Stream fileContents = httpContent.ReadAsStream())
                {
                    fileContents.CopyTo(imagem);
                }
            }

            Produto produto = new Produto()
            {
                Nome = dict["Nome"],
                Marca = dict["Marca"],
                Fabricante = dict["Fabricante"],
                Ano = Convert.ToInt32(dict["AnoFabricacao"]),
                IDCategoria = Convert.ToInt32(dict["IDCategoria"]),
                Descricao = dict["Descricao"],
                Imagem = imagem.ToArray()
            };
            if(!produto.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar ao banco!" }.ToJSON());
                return;
            }

            Console.WriteLine(JsonConvert.SerializeObject(dict));

        }
    }
}
