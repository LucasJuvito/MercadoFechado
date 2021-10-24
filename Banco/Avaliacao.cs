using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Avaliacao
    {
        public long IDVenda;
        public double Pontuacao;
        public string Comentario;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO avaliacao (pontuacao, comentario, id_venda) " +
                    "VALUES (@pontuacao, @comentario, @id_venda);";
                command.Parameters.AddWithValue("@id_venda", IDVenda);
                command.Parameters.AddWithValue("@pontuacao", Pontuacao);
                command.Parameters.AddWithValue("@comentario", Comentario);

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

        public bool DeletarDoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM avaliacao WHERE id_venda = @id";
                command.Parameters.AddWithValue("@id", IDVenda);

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

        public static Avaliacao BuscarPorID(long idVenda)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT pontuacao, comentario FROM venda WHERE id_venda = @id;";
            command.Parameters.AddWithValue("@id", idVenda);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            Avaliacao ret = new Avaliacao()
            {
                IDVenda = idVenda,
                Pontuacao = reader.GetDouble(0),
                Comentario = reader.GetString(1)
            };
            return ret;
        }

        public static bool AtualizarComentario(long idVenda, string comentario)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE avaliacao SET comentario = @comentario WHERE id_venda = @id;";
                command.Parameters.AddWithValue("@comentario", comentario);
                command.Parameters.AddWithValue("@id", idVenda);

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

        public static bool AtualizarPontuacao(long idVenda, int pontuacao)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE avaliacao SET pontuacao = @pontuacao WHERE id_venda = @id;";
                command.Parameters.AddWithValue("@pontuacao", pontuacao);
                command.Parameters.AddWithValue("@id", idVenda);

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

        public static List<Avaliacao> ListarPorAnuncio(long idAnuncio)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT pontuacao, comentario, avaliacao.id_venda " +
                "FROM avaliacao " +
                "JOIN venda ON avaliacao.id_venda = venda.id_venda " +
                "WHERE venda.id_anuncio = @idAnuncio " +
                "ORDER BY avaliacao.id_venda DESC";
            command.Parameters.AddWithValue("@idAnuncio", idAnuncio);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Avaliacao> ret = new List<Avaliacao>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Avaliacao avaliacao = new Avaliacao
                {
                    Pontuacao = reader.GetDouble(0),
                    Comentario = reader.GetString(1),
                    IDVenda = reader.GetInt64(2)
                };
                ret.Add(avaliacao);
            }
            return ret;
        }
    }
}
