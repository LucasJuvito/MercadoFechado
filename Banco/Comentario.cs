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

        public static List<Comentario> BuscarComentariosPorAnuncio(long idAnuncio)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_comentario, id_user_comum, descricao FROM comentarios WHERE id_anuncio = @id";
            command.Parameters.AddWithValue("@id", idAnuncio);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Comentario> ret = new List<Comentario>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Comentario comentario = new Comentario
                {
                    ID = reader.GetInt64(0),
                    IDUserComum = reader.GetInt64(1),
                    Descricao = reader.GetString(2),
                    IDAnuncio = idAnuncio
                };
                ret.Add(comentario);
            }
            return ret;
        }

        public static bool AtualizarDescricao(long id, string descricao)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE comentarios SET descricao = @descricao WHERE id_comentario = @id;";
                command.Parameters.AddWithValue("@descricao", descricao);
                command.Parameters.AddWithValue("@id", id);

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
