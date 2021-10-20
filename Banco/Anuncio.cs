using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Anuncio
    {
        public long ID;
        public double Valor;
        public string Titulo;
        public string Descricao;
        public long IDProduto;
        public long IDVendedor;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor) " +
                    "VALUES (@titulo, @valor, @descricao, @id_produto, @id_vendedor);";
                command.Parameters.AddWithValue("@titulo", Titulo);
                command.Parameters.AddWithValue("@valor", Valor);
                command.Parameters.AddWithValue("@descricao", Descricao);
                command.Parameters.AddWithValue("@id_produto", IDProduto);
                command.Parameters.AddWithValue("@id_vendedor", IDVendedor);

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

        public static bool AtualizarTitulo(long idAnuncio, string titulo)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE anuncio SET titulo = @titulo WHERE id_anuncio = @id";
                command.Parameters.AddWithValue("@titulo", titulo);
                command.Parameters.AddWithValue("@id", idAnuncio);

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

        public static bool AtualizarDescricao(long idAnuncio, string descricao)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE anuncio SET descricao = @descricao WHERE id_anuncio = @id";
                command.Parameters.AddWithValue("@descricao", descricao);
                command.Parameters.AddWithValue("@id", idAnuncio);

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

        public static List<Anuncio> ListarPorCategoria(long idCategoria)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_anuncio, valor, titulo, anuncio.descricao, produto.id_produto, id_vendedor " +
                "FROM anuncio " +
                "JOIN produto " +
                "ON anuncio.id_produto = produto.id_produto " +
                "WHERE produto.id_categoria = @idCategoria;";
            command.Parameters.AddWithValue("@idCategoria", idCategoria);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Anuncio> ret = new List<Anuncio>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Anuncio anuncio = new Anuncio
                {
                    ID = reader.GetInt64(0),
                    Valor = reader.GetDouble(1),
                    Titulo = reader.GetString(2),
                    Descricao = reader.GetString(3),
                    IDProduto = reader.GetInt64(4),
                    IDVendedor = reader.GetInt64(5)
                };
                ret.Add(anuncio);
            }
            return ret;
        }

        public static Anuncio BuscarPorID(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT titulo, valor, anuncio.descricao, produto.id_produto, id_vendedor " +
                "FROM anuncio " +
                "JOIN produto " +
                "ON anuncio.id_produto = produto.id_produto " +
                "WHERE id_anuncio = @id LIMIT 1;";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            Anuncio ret = new Anuncio()
            {
                ID = id,
                Titulo = reader.GetString(0),
                Valor = reader.GetDouble(1),
                Descricao = reader.GetString(2),
                IDProduto = reader.GetInt64(3),
                IDVendedor = reader.GetInt64(4)
            };
            return ret;
        }
    }
}
