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
                sqlConnection.Close();

            }

        }




        // insert user info into db
        // return true if insert is succeessful
        // make sure if user doesn't exist by using the above function
        public bool insertNewUser(string userName, string psw, SqlConnection sqlConnection)
        {

            try
            {

                string sqlString = "INSERT INTO Users (UserName, Password) VALUES (@UserName, @psw)";


                SqlCommand myCommand = new SqlCommand(sqlString, sqlConnection);

                myCommand.Parameters.AddWithValue("@UserName", userName);
                myCommand.Parameters.AddWithValue("@psw", psw);
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
                sqlConnection.Close();

            }

        }

    }
}
