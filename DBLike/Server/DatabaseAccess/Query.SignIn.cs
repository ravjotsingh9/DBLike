using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Server.DatabaseAccess
{
    public partial class Query
    {
        public bool checkAuthentication(string username, string password, SqlConnection sqlConnection)
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Users where UserName=@UserName and Password=@Password", sqlConnection);
            myCommand.Parameters.AddWithValue("@UserName", username);
            myCommand.Parameters.AddWithValue("@Password", password);
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