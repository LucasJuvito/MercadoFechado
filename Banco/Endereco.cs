using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes.Banco
{
    class Endereco
    {
        public long ID;
        public string CEP;
        public string Estado;
        public string Cidade;
        public string Bairro;
        public string Quadra;
        public int Numero;
        public string Complemento;
        public long IDProprietario;

        public bool AdicionarAoBanco()
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO endereco (cep, estado, cidade, bairro, quadra, numero, complemento, id_proprietario) " +
                    "VALUES (@cep, @estado, @cidade, @bairro, @quadra, @numero, @complemento, @id_proprietario); ";
                command.Parameters.AddWithValue("@cep", CEP);
                command.Parameters.AddWithValue("@estado", Estado);
                command.Parameters.AddWithValue("@cidade", Cidade);
                command.Parameters.AddWithValue("@bairro", Bairro);
                command.Parameters.AddWithValue("@quadra", Quadra);
                command.Parameters.AddWithValue("@numero", Numero);
                command.Parameters.AddWithValue("@complemento", Complemento);
                command.Parameters.AddWithValue("@id_proprietario", IDProprietario);

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

        public static List<Endereco> BuscarPorProprietario(long id)
        {
            using MySqlConnection connection = new MySqlConnection(Global.DBConnectionBuilder.ConnectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id_endereco, cep, estado, cidade, bairro, quadra, numero, complemento, id_proprietario FROM endereco " +
                "JOIN usuario_comum ON id_proprietario = id_user_comum WHERE id_user_comum = @id";
            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = command.ExecuteReader();
            List<Endereco> ret = new List<Endereco>();

            while(reader.Read())
            {
                if (!reader.HasRows)
                    return ret;

                Endereco endereco = new Endereco
                {
                    ID = reader.GetInt64(0),
                    CEP = reader.GetString(1),
                    Estado = reader.GetString(2),
                    Cidade = reader.GetString(3),
                    Bairro = reader.GetString(4),
                    Quadra = reader.GetString(5),
                    Numero = reader.GetInt32(6),
                    Complemento = reader.IsDBNull(7) ? null : reader.GetString(7),
                    IDProprietario = reader.GetInt64(8)
                };
                ret.Add(endereco);
            }
            return ret;
        }
    }
}
