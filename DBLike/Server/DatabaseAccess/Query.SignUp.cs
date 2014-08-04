using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace Server.DatabaseAccess
{
    public partial class Query
    {

        // return true if user exists
        // thread should use this first so we can know the reason is whether user exists or sth. else
        public bool checkIfUserExists(string userName, SqlConnection sqlConnection)
        {

            try
            {
                SqlDataReader myReader = null;


                SqlCommand myCommand = new SqlCommand("select * from dbo.Users where UserName = @UserName", sqlConnection);

                myCommand.Parameters.AddWithValue("@UserName", userName);

                myReader = myCommand.ExecuteReader();

                // if user exists
                if (myReader.Read())
                {
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return true;
            }
            finally
            {
                //sqlConnection.Close();

            }

        }




        // insert user info into db
        // return true if insert is succeessful
        // make sure if user doesn't exist first by using the above function
        public bool insertNewUser(string userName, string psw, SqlConnection sqlConnection)
        {

            try
            {

                string sqlString = "INSERT INTO Users (UserName, Password) VALUES (@UserName, @psw)";


                SqlCommand myCommand = new SqlCommand(sqlString, sqlConnection);
                SHA1 sha1Pass = SHA1.Create();
                StringBuilder sb = new StringBuilder();
                foreach (Byte b in sha1Pass.ComputeHash(Encoding.UTF8.GetBytes(psw)))
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
                string Pass= Convert.ToBase64String(buffer);

                myCommand.Parameters.AddWithValue("@UserName", userName);
                myCommand.Parameters.AddWithValue("@psw", Pass);
                // insert into the db
                myCommand.ExecuteNonQuery();



                // check if insert successfully
                if (checkIfUserExists(userName, sqlConnection))
                {
                    return true;
                }
                else
                {

                    return false;

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                //sqlConnection.Close();

            }

        }

    }
}
