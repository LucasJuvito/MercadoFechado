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
        public long ID;
        public int Pontuacao;
        public string Comentario;
        public long IDUserComum;
        public long IDVenda;


        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO avaliacao (pontuacao, comentario, id_user_comum, id_venda) " +
                    "VALUES (@pontuacao, @comentario, @id_user_comum, @id_venda);";
                command.Parameters.AddWithValue("@pontuacao", Pontuacao);
                command.Parameters.AddWithValue("@comentario", Comentario);
                command.Parameters.AddWithValue("@id_user_comum", IDUserComum);
                command.Parameters.AddWithValue("@id_venda", IDVenda);

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
