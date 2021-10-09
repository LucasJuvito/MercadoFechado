using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class AcessoUsuario
    {
        public long IDUsuarioComum { get; set; }
        public string Token { get; set; }

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO acesso_usuario (id_user_comum, token) " +
                    "VALUES (@id_user_comum, @token);";
                command.Parameters.AddWithValue("@id_user_comum", IDUsuarioComum);
                command.Parameters.AddWithValue("@token", Token);

                if (command.ExecuteNonQuery() != 1)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
