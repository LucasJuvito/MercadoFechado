using MySqlConnector;
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
            Global.Load();

            APIServer api = new APIServer(1890);

            api.AddAction("/usuarios/porcpf", Handlers.Usuarios.BuscarPorCPF.ProcessContext);

            api.Listen();
        }
    }
}
