using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServidorTestes
{
    class Utils
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public static string GerarToken()
        {
            byte[] buffer = new byte[16];
            rng.GetNonZeroBytes(buffer);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
