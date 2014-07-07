using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.ConnectionManager;
using System.Data.SqlClient;

namespace Server.DatabaseAccess
{
    public partial class Query
    {
        /// <summary>
        /// insert new file data into database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="hashVaule"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public bool insertNewFileData(string userName, string fileName, string filePath, string hashVaule, DateTime timeStamp)
        {

            DataBaseConn dbconn = new DataBaseConn(1);
            try
            {
                SqlConnection conn = dbconn.DBConnect();
                string queryString = "INSERT INTO dbo.FilesInfo"+
                                     " (FileName, TimeStamp, HashValue, FilePath, UserName) VALUES"+
                                     " (@FileName, @TimeStamp, @HashValue, @FilePath, @UserName)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@FilePath", filePath);
                command.Parameters.AddWithValue("@FileName", fileName);
                command.Parameters.AddWithValue("@TimeStamp", timeStamp);
                command.Parameters.AddWithValue("@HashValue", hashVaule);
                Int32 rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("RowsAffected: {0}", rowsAffected);
                if (rowsAffected == 0)
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                dbconn.DBClose();
                
            }
            return true;
        }
        
        /// <summary>
        /// update file data in the databse
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="filePath"></param>
        /// <param name="hashVaule"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public bool updateFilesData(string userName, string filePath, string hashVaule, DateTime timeStamp)
        {
            DataBaseConn dbconn = new DataBaseConn(1);
            try
            {
                SqlConnection conn =  dbconn.DBConnect();
                string queryString = "UPDATE Users SET TimeStamp=@TimeStamp, HashValue=@HashValue WHERE UserName=@UserName AND FilePath=@FilePath ";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@TimeStamp", timeStamp);
                command.Parameters.AddWithValue("@HashValue", hashVaule);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@FilePath", filePath);
                Int32 rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("RowsAffected: {0}", rowsAffected);
                if (rowsAffected == 0)
                {
                    return false;
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                dbconn.DBClose();  
            }
            return true;
        }


        /// <summary>
        /// If file already exists in the database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="filePath"></param>
        /// <returns>true/false</returns>
        public bool fileAlreadyExist(string userName,string filePath)
        {

            DataBaseConn dbconn = new DataBaseConn(1);
            try
            {
                SqlConnection conn = dbconn.DBConnect();
                string queryString = "SELECT * FROM dbo.FilesInfo WHERE UserName =@UserName AND FilePath = @FilePath";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@FilePath", filePath);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
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
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                dbconn.DBClose();
                
            }
        }
    }
}
