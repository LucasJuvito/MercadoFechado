using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class VendaCompleta
    {
        public long IDVenda { get; set; }
        public long IDProduto { get; set; }
        public long IDAnuncio { get; set; }
        public long IDVendedor { get; set; }
        public long IDComprador { get; set; }
        public string Titulo { get; set; }
        public DateTime HorarioVenda { get; set; }
        public double Pontuacao { get; set; }

        public static List<VendaCompleta> ListarComprasPessoais(long idUser)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_venda, id_produto, id_anuncio, vendedor, comprador, titulo, venda_hora, pontuacao FROM dados_agrupados_venda " +
                "WHERE comprador = @idUser";
            command.Parameters.AddWithValue("@idUser", idUser);

            using MySqlDataReader reader = command.ExecuteReader();
            List<VendaCompleta> ret = new List<VendaCompleta>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                VendaCompleta venda = new VendaCompleta
                {
                    IDVenda = reader.GetInt64(0),
                    IDProduto = reader.GetInt64(1),
                    IDAnuncio = reader.GetInt64(2),
                    IDVendedor = reader.GetInt64(3),
                    IDComprador = reader.GetInt64(4),
                    Titulo = reader.GetString(5),
                    HorarioVenda = reader.GetDateTime(6),
                    Pontuacao = reader.GetDouble(7)
                };
                ret.Add(venda);
            }
            return ret;
        }

        public static List<VendaCompleta> ListarVendasPessoais(long idUser)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_venda, id_produto, id_anuncio, vendedor, comprador, titulo, venda_hora, pontuacao FROM dados_agrupados_venda " +
                "WHERE vendedor = @idUser";
            command.Parameters.AddWithValue("@idUser", idUser);

            using MySqlDataReader reader = command.ExecuteReader();
            List<VendaCompleta> ret = new List<VendaCompleta>();

            while (reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                VendaCompleta venda = new VendaCompleta
                {
                    IDVenda = reader.GetInt64(0),
                    IDProduto = reader.GetInt64(1),
                    IDAnuncio = reader.GetInt64(2),
                    IDVendedor = reader.GetInt64(3),
                    IDComprador = reader.GetInt64(4),
                    Titulo = reader.GetString(5),
                    HorarioVenda = reader.GetDateTime(6),
                    Pontuacao = reader.GetDouble(7)
                };
                ret.Add(venda);
            }
            return ret;
        }
    }
}
