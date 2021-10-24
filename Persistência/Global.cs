using System;
using System.IO;
using System.Data.Common;
using MySqlConnector;

namespace ServidorTestes
{
    class Global
    {
        public static MySqlConnectionStringBuilder DBConnectionBuilder;

        public static void Load()
        {
            DBConnectionBuilder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                UserID = "root",
                Password = "20161140040126",
                Database = "mercado_fechado",
                AllowUserVariables = true
            };
        }
    }
}
