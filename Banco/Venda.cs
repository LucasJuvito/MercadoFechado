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
        public double Valor;
        public DateTime Data;
        public long IDAnuncio;
        public long IDEndereco;
        public long IDVendedor;
        public long IDComprador;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio) " +
                    "VALUES (@venda_hora, @vendedor, @comprador, @valor, @endereco_entrega, @id_anuncio);";
                command.Parameters.AddWithValue("@valor", Valor);
                command.Parameters.AddWithValue("@venda_hora", Data);
                command.Parameters.AddWithValue("@vendedor", IDVendedor);
                command.Parameters.AddWithValue("@id_anuncio", IDAnuncio);
                command.Parameters.AddWithValue("@comprador", IDComprador);
                command.Parameters.AddWithValue("@endereco_entrega", IDEndereco);

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

        public static List<Venda> ListarVendas(long idAnuncio)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_venda, valor, venda_hora, endereco_entrega, vendedor, comprador " +
                "FROM venda " +
                "WHERE id_anuncio = @idAnuncio";
            command.Parameters.AddWithValue("@idAnuncio", idAnuncio);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Venda> ret = new List<Venda>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Venda venda = new Venda
                {
                    ID = reader.GetInt64(0),
                    Valor = reader.GetDouble(1),
                    Data = reader.GetDateTime(2),
                    IDEndereco = reader.GetInt64(3),
                    IDVendedor = reader.GetInt64(4),
                    IDComprador = reader.GetInt64(5)
                };
                ret.Add(venda);
            }
            return ret;
        }
    }
}
