using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace techical.test.Utils
{
    public static class PasswordGenerator
    {

        public static String EncryptPassword(string userPassword)
        {

            byte[] data = System.Text.Encoding.ASCII.GetBytes(userPassword);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }
    }
}