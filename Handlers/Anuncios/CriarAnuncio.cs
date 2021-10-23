using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;

namespace ServidorTestes.Handlers.Anuncios
{
    class CriarAnuncio
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            var data = context.Request.InputStream;
            var streamContent = new StreamContent(data);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(context.Request.ContentType);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var provider = streamContent.ReadAsMultipartAsync().Result;
            
            foreach (var httpContent in provider.Contents)
            {
                var fileName = httpContent.Headers.ContentDisposition.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    dict.Add(httpContent.Headers.ContentDisposition.Name.Replace("\"", ""), httpContent.ReadAsStringAsync().Result.Replace("\"", ""));
                    continue;
                }
            }

            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(dict["token_acesso"]);
            if (usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            Anuncio anuncio = new Anuncio()
            {
                Titulo = dict["titulo_anuncio"],
                Valor = Double.Parse(dict["valor_anuncio"]),
                Descricao = dict["descricao_anuncio"],
                IDProduto = Int64.Parse(dict["id_produto"]),
                IDVendedor = usuarioLogado.IDUsuarioComum
            };
            if (!anuncio.AdicionarAoBanco())
            {
                writer.WriteLine(new BaseResponse() { Message = "Não foi possível adicionar ao banco!" }.ToJSON());
                return;
            }

            context.Response.Redirect(context.Request.UrlReferrer + "");
        }
    }
}
