using ServidorTestes.Banco;
using ServidorTestes.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Handlers.Usuarios
{
    class BuscarSaldo
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

            UsuarioComum usuario = UsuarioComum.BuscarPorID(usuarioLogado.IDUsuarioComum);
            if(usuario == null)
            {
                writer.WriteLine(new BaseResponse() { Message = "Usuário não encontrado!" }.ToJSON());
                return;
            }

            BuscarSaldoUsuarioResponse response = new BuscarSaldoUsuarioResponse()
            {
                Success = true,
                Message = "Saldo obtido com sucesso.",
                Saldo = usuario.Saldo
            };
            writer.WriteLine(response.ToJSON());
            return;
        }
    }
}
