using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Security
{
    public static class PasswordHelper
    {
        public static string  EncodePasswordMD5(string password)
        {
            Byte[] originalByte;
            Byte[] encodeByte;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalByte=ASCIIEncoding.Default.GetBytes(password);
            encodeByte=md5.ComputeHash(originalByte);
            return BitConverter.ToString(encodeByte);
        }
    }
}
