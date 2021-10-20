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

        public static AcessoUsuario BuscarToken(string token)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_user_comum " +
                "FROM acesso_usuario " +
                "WHERE token = @token LIMIT 1;";
            command.Parameters.AddWithValue("@token", token);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            AcessoUsuario ret = new AcessoUsuario()
            {
                IDUsuarioComum = reader.GetInt64(0),
                Token = token
            };
            return ret;
        }
    }
}
