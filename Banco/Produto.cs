using MySqlConnector;
using System;
using System.Collections.Generic;
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

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, descricao) " +
                    "VALUES (@nome, @marca, @fabricante, @ano, @id_categoria, @descricao);";
                command.Parameters.AddWithValue("@nome", Nome);
                command.Parameters.AddWithValue("@marca", Marca);
                command.Parameters.AddWithValue("@fabricante", Fabricante);
                command.Parameters.AddWithValue("@ano", Ano);
                command.Parameters.AddWithValue("@id_categoria", IDCategoria);
                command.Parameters.AddWithValue("@descricao", Descricao);

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
            command.CommandText = "SELECT id_produto, produto.nome, marca, fabricante, ano_fabricacao, descricao FROM produto " +
                "JOIN categoria ON produto.id_categoria = categoria.id_categoria WHERE categoria.nome = @nome";
            command.Parameters.AddWithValue("@nome", nome);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Produto> ret = new List<Produto>();

            while(reader.Read())
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
                    Descricao = reader.GetString(5)
                };
                ret.Add(produto);
            }
            return ret;
        }
    }
}
