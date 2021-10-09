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
        public long ID;
        public string CPF;
        public string Nome;
        public DateTime DataNascimento;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento) " +
                    "VALUES (@id, @cpf, @nome, @nascimento);";
                command.Parameters.AddWithValue("@id", ID);
                command.Parameters.AddWithValue("@cpf", CPF);
                command.Parameters.AddWithValue("@nome", Nome);
                command.Parameters.AddWithValue("@nascimento", DataNascimento);

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

        public static PessoaFisica BuscarPeloID(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT cpf, nome, data_nascimento FROM usuario_pes_fisica WHERE id_pes_fisica = @id;";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            if (!reader.HasRows)
                return null;

            PessoaFisica ret = new PessoaFisica()
            {
                ID = id,
                CPF = reader.GetString(0),
                Nome = reader.GetString(1),
                DataNascimento = reader.GetDateTime(2)
            };
            return ret;
        }
    }
}
