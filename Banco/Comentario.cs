using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Comentario
    {
        public long ID;
        public string Descricao;
        public long IDUserComum;
        public long IDAnuncio;


        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) " +
                    "VALUES (@descricao, @id_user_comum, @id_anuncio);";
                command.Parameters.AddWithValue("@descricao", Descricao);
                command.Parameters.AddWithValue("@id_user_comum", IDUserComum);
                command.Parameters.AddWithValue("@id_anuncio", IDAnuncio);

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

    }
}
