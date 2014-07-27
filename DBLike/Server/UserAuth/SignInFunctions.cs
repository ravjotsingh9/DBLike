using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Server.UserAuth
{
    class SignInFunctions
    {
        public bool userAuthentication(string username, string password)
        {
            ConnectionManager.DataBaseConn con = new ConnectionManager.DataBaseConn(1);
            SqlConnection sql=con.DBConnect();
            DatabaseAccess.Query query = new DatabaseAccess.Query();
            bool status= query.checkAuthentication(username, password,sql);
            return status;
        }
    }
}
