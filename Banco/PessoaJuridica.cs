using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class PessoaJuridica
    {
        public long ID;
        public string CNPJ;
        public long IDEnderecoSede;
        public string NomeFantasia;

        public static PessoaJuridica BuscarPeloID(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT cnpj, endereco_sede, nome_fantasia FROM usuario_pes_juridica WHERE id_pes_juridica = @id;";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            PessoaJuridica ret = new PessoaJuridica()
            {
                ID = id,
                CNPJ = reader.GetString(0),
                IDEnderecoSede = reader.GetInt64(1),
                NomeFantasia = reader.GetString(2)
            };
            return ret;
        }
    }
}
