using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Handlers.Vendas
{
    class ListarVendasPessoais
    {
        public static void ProcessContext(HttpListenerContext context, StreamWriter writer, StreamReader reader)
        {
            string token = context.Request.Headers.Get("Authorization");

            AcessoUsuario usuarioLogado = AcessoUsuario.BuscarToken(token);
            if (usuarioLogado == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não está logado!" }.ToJSON());
                return;
            }

            List<VendaCompleta> vendas = VendaCompleta.ListarVendasPessoais(usuarioLogado.IDUsuarioComum);
            ListarVendasCompletasResponse response = new ListarVendasCompletasResponse()
            {
                Vendas = vendas,
                Success = true,
                Message = "Vendas obtidas com sucesso!"
            };
            writer.WriteLine(response.ToJSON());
        }
    }
}
