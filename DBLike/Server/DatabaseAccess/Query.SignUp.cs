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



    }
}
