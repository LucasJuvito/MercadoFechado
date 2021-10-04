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
                command.CommandText = "INSERT INTO anuncio (titulo, descricao, id_produto, id_vendedor) " +
                    "VALUES (@titulo, @descricao, @id_produto, @id_vendedor);";
                command.Parameters.AddWithValue("@titulo", Titulo);
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

    }
}
