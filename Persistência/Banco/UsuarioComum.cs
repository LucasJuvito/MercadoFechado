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
        public long ID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public double Saldo { get; set; }

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO usuario_comum (login, senha, saldo) VALUES (@login, @senha, 0);";
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

        public static bool CriarPessoaFisica(string login, string senha, string cpf, string nome, DateTime? dataNascimento)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "CALL cadastrar_atomico(FALSE, @login, @senha, @cpf, @nome, @nascimento)";
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@senha", senha);
            command.Parameters.AddWithValue("@cpf", cpf);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@nascimento", dataNascimento);

            return command.ExecuteNonQuery() > 0;
        }

        public static bool CriarPessoaJuridica(string login, string senha, string cnpj, string nomeFantasia)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "CALL cadastrar_atomico(TRUE, @login, @senha, @cnpj, @nomeFantasia, NULL)";
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@senha", senha);
            command.Parameters.AddWithValue("@cnpj", cnpj);
            command.Parameters.AddWithValue("@nomeFantasia", nomeFantasia);

            return command.ExecuteNonQuery() > 0;
        }

        public static UsuarioComum BuscarPorID(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT login, senha, saldo FROM usuario_comum WHERE id_user_comum = @id;";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            UsuarioComum ret = new UsuarioComum()
            {
                ID = id,
                Login = reader.GetString(0),
                Senha = reader.GetString(1),
                Saldo = reader.GetDouble(2)
            };
            return ret;
        }

        public static UsuarioComum BuscarPorLogin(string login)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_user_comum, senha, saldo FROM usuario_comum WHERE login = @login;";
            command.Parameters.AddWithValue("@login", login);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            UsuarioComum ret = new UsuarioComum()
            {
                ID = reader.GetInt64(0),
                Login = login,
                Senha = reader.GetString(1),
                Saldo = reader.GetDouble(2)
            };
            return ret;
        }
    }
}
