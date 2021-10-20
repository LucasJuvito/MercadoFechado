using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Produto
    {
        public long ID;
        public string Nome;
        public string Marca;
        public string Fabricante;
        public int Ano;
        public long IDCategoria;
        public string Descricao;
        public byte[] Imagem;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao, foto) " +
                    "VALUES (@nome, @marca, @fabricante, @ano, @id_categoria, @descricao, @foto);";
                command.Parameters.AddWithValue("@nome", Nome);
                command.Parameters.AddWithValue("@marca", Marca);
                command.Parameters.AddWithValue("@fabricante", Fabricante);
                command.Parameters.AddWithValue("@ano", Ano);
                command.Parameters.AddWithValue("@id_categoria", IDCategoria);
                command.Parameters.AddWithValue("@descricao", Descricao);
                command.Parameters.AddWithValue("@foto", Imagem);

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

        public static List<Produto> BuscarPorCategoria(string nome)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_produto, produto.nome, marca, fabricante, ano_fabricacao, descricao, id_categoria FROM produto " +
                "JOIN categoria ON produto.id_categoria = categoria.id_categoria WHERE categoria.nome = @nome";
            command.Parameters.AddWithValue("@nome", nome);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Produto> ret = new List<Produto>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Produto produto = new Produto
                {
                    ID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Marca = reader.GetString(2),
                    Fabricante = reader.GetString(3),
                    Ano = reader.GetInt32(4),
                    Descricao = reader.GetString(5),
                    IDCategoria = reader.GetInt64(6)
                };
                ret.Add(produto);
            }
            return ret;
        }

        public static Produto BuscarPorID(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_produto, produto.nome, marca, fabricante, ano_fabricacao, descricao " +
                "FROM produto " +
                "WHERE id_produto = @id";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            Produto produto = new Produto
            {
                ID = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Marca = reader.GetString(2),
                Fabricante = reader.GetString(3),
                Ano = reader.GetInt32(4),
                Descricao = reader.GetString(5)
            };
            return produto;
        }

        public static Produto BuscarPorIDComImagem(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_produto, produto.nome, marca, fabricante, ano_fabricacao, descricao, foto " +
                "FROM produto " +
                "WHERE id_produto = @id";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            using MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[1024];
            long offset = 0;
            long readCnt;

            while((readCnt = reader.GetBytes(6, offset, buffer, 0, 1024)) > 0)
            {
                offset += readCnt;
                memoryStream.Write(buffer, 0, (int)readCnt);
            }

            Produto produto = new Produto
            {
                ID = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Marca = reader.GetString(2),
                Fabricante = reader.GetString(3),
                Ano = reader.GetInt32(4),
                Descricao = reader.GetString(5),
                Imagem = memoryStream.ToArray()
            };
            return produto;
        }
    }
}
