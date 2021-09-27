using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class PessoaFisica
    {
        public int ID;
        public string CPF;
        public string Nome;
        public DateTime DataNascimento;

        public static PessoaFisica BuscarPeloCPF(string cpf)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_pes_fisica, nome, data_nascimento FROM usuario_pes_fisica WHERE cpf = @cpf;";
            command.Parameters.AddWithValue("@cpf", cpf);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            PessoaFisica ret = new PessoaFisica()
            {
                ID = reader.GetInt32(0),
                CPF = cpf,
                Nome = reader.GetString(1),
                DataNascimento = reader.GetDateTime(2)
            };
            return ret;
        }
    }
}
