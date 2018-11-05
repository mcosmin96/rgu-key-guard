using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;


namespace db_access
{
    //This class will be used by the Manager to make calls to the database so it should be populated only by functions the Manager needs to know about
    public class DBAccess
    {
        SqlConnection dbConnection;
        public SqlConnection getConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqlConnection(getConnectionSB().ConnectionString);
            }
            return dbConnection;
        }

        //Pull all keys from database operation (currently returning a mock key array)
        private SqlConnectionStringBuilder getConnectionSB()
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = ConfigurationManager.AppSettings["DB_SOURCE"];
            cb.UserID = ConfigurationManager.AppSettings["DB_USER_ID"];
            cb.Password = ConfigurationManager.AppSettings["DB_PASSWORD"];
            cb.InitialCatalog = ConfigurationManager.AppSettings["DB_NAME"];
            return cb;
        }
    }
}
