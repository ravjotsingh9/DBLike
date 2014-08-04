using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace Server.DatabaseAccess
{
    public partial class Query
    {
        public bool checkAuthentication(string username, string password, SqlConnection sqlConnection)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Users where UserName=@UserName and Password=@Password", sqlConnection);
            SHA1 sha1Pass = SHA1.Create();
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in sha1Pass.ComputeHash(Encoding.UTF8.GetBytes(password)))
            {
                sb.Append(b.ToString("X2"));
            }
            string hexString = sb.ToString();

            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i++)
            {
                buffer[i / 2] = Convert.ToByte(Convert.ToInt32(hexString.Substring(i, 2), 16));
                i += 1;
            }
            string Pass = Convert.ToBase64String(buffer);
            myCommand.Parameters.AddWithValue("@UserName", username);
            myCommand.Parameters.AddWithValue("@Password", Pass);
            myReader = myCommand.ExecuteReader();
            if(myReader.HasRows)
            {
                return true;
            }
            else
                return false;
        }
    }
}