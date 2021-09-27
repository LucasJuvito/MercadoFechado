using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class UsuarioComum
    {
        public long ID;
        public string Login;
        public string Senha;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO usuario_comum (login, senha) VALUES (@login, @senha);";
                command.Parameters.AddWithValue("@login", Login);
                command.Parameters.AddWithValue("@senha", Senha);

                if (command.ExecuteNonQuery() != 1)
                    return false;

                ID = command.LastInsertedId;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static UsuarioComum BuscarPorID(int id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT login, senha FROM usuario_comum WHERE id_user_comum = @id;";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            UsuarioComum ret = new UsuarioComum()
            {
                ID = id,
                Login = reader.GetString(0),
                Senha = reader.GetString(1)
            };
            return ret;
        }

        public static UsuarioComum BuscarPorLogin(string login)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_user_comum, senha FROM usuario_comum WHERE login = @login;";
            command.Parameters.AddWithValue("@login", login);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            UsuarioComum ret = new UsuarioComum()
            {
                ID = reader.GetInt64(0),
                Login = login,
                Senha = reader.GetString(1)
            };
            return ret;
        }
    }
}
