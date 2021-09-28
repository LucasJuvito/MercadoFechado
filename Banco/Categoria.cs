using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Categoria
    {
        public long ID;
        public string Nome;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO categoria (nome) VALUES (@nome);";
                command.Parameters.AddWithValue("@nome", Nome);

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

        public static List<Categoria> BuscarTodas()
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_categoria, nome FROM categoria;";

            using MySqlDataReader reader = command.ExecuteReader();
            List<Categoria> ret = new List<Categoria>();

            while(reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Categoria categoria = new Categoria
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                };
                ret.Add(categoria);
            }
            return ret;
        }

        public static Categoria BuscarID(string categoria)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_categoria FROM categoria WHERE nome = @nome;";
            command.Parameters.AddWithValue("@nome", categoria);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            Categoria ret = new Categoria()
            {
                ID = reader.GetInt64(0)
            };
            return ret;
        }
    }
}
