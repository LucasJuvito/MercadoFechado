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
        public int ID;
        public string Nome;

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
    }
}
