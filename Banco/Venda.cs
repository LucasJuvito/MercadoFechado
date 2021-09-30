using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Venda
    {
        public long ID;
        public DateTime Data;
        public long Vendedor;
        public long Comprador;
        public int Valor;
        public long Endereco;
        public long Anuncio;


        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio) " +
                    "VALUES (@venda_hora, @vendedor, @comprador, @valor, @endereco_entrega, @id_anuncio);";
                command.Parameters.AddWithValue("@venda_hora", Data);
                command.Parameters.AddWithValue("@vendedor", Vendedor);
                command.Parameters.AddWithValue("@comprador", Comprador);
                command.Parameters.AddWithValue("@valor", Valor);
                command.Parameters.AddWithValue("@endereco_entrega", Endereco);
                command.Parameters.AddWithValue("@id_anuncio", Anuncio);

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
