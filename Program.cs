using MySqlConnector;
using Newtonsoft.Json;
using ServidorTestes.Banco;
using ServidorTestes.Requests;
using ServidorTestes.Responses;
using System;
using System.IO;
using System.Net;

namespace ServidorTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(JsonConvert.SerializeObject(DateTime.Now));
            Global.Load();

            APIServer api = new APIServer(1890);

            api.AddAction("/usuarios/porcpf", Handlers.Usuarios.BuscarPorCPF.ProcessContext);
            api.AddAction("/usuarios/criar/pessoafisica", Handlers.Usuarios.CriarPessoaFisica.ProcessContext);

            api.AddAction("/categorias/listar", Handlers.Categorias.ListarCategorias.ProcessContext);

            api.AddAction("/produtos/porcategoria", Handlers.Produtos.BuscarProdutosPorCategoria.ProcessContext);
            api.AddAction("/produtos/criar", Handlers.Produtos.CriarProduto.ProcessContext);

            api.Listen();
        }
    }
}
