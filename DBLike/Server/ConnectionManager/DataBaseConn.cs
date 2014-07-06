using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConnectionManager
{
    class DataBaseConn
    {
        /// <summary>
        /// Database Info
        /// </summary>
        //private string userName;
        //private string password ;
        //private string dataSource ;
        private SqlConnection conn;
        private SqlConnectionStringBuilder connString1Builder;

        /// <summary>
        ///  Constructor --- setup information for DB
        /// </summary>
        /// <param name="dbNumber">specift which DB you want to connect
        ///                        e.g. 1 for database --- cics525Group3@n67s8dtv4s.database.windows.net
        /// </param>
        public DataBaseConn(int dbNumber)
        {
            DBNo(dbNumber);
        }

        /// <summary>
        /// initialize database info according to the dbNumber
        /// </summary>
        /// <param name="number"></param>
        private void DBNo(int number)
        {
            if (number == 1)
            {
                connString1Builder = new SqlConnectionStringBuilder();
                connString1Builder.DataSource = "n67s8dtv4s.database.windows.net";
                connString1Builder.InitialCatalog = "cics525Group3";
                connString1Builder.Encrypt = true;
                connString1Builder.TrustServerCertificate = false;
                connString1Builder.UserID = "cics525@n67s8dtv4s";
                connString1Builder.Password = "Account1*";
                connString1Builder.ConnectTimeout = 60;
            }

        }

        /// <summary>
        /// Start Database connection
        /// </summary>
        /// <returns>
        /// SqlConnection
        /// </returns>
        public SqlConnection DBConnect()
        {
            conn = new SqlConnection(connString1Builder.ToString());
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Close database connection 
        /// </summary>
        /// <param name="conn"> connection for database</param>
        public void DBClose()
        {
            conn.Close();
        }

    }
}
