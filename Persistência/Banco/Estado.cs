using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Estado
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public static List<Estado> ListarTodos()
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT sigla, descricao FROM estado";

            using MySqlDataReader reader = command.ExecuteReader();
            List<Estado> ret = new List<Estado>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Estado endereco = new Estado
                {
                    Sigla = reader.GetString(0),
                    Descricao = reader.GetString(1)
                };
                ret.Add(endereco);
            }
            return ret;
        }
    }
}
