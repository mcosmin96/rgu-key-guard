using db_access;
using System;
using System.Data.SqlClient;
using System.Text;


namespace manager
{
    //This class will be used by the User Interface to make calls to the Manager so it should be populated only by functions the Manager performs for the UI
    public class Manager
    {
        private SqlConnection connection;

        public Manager()
        {
            DBAccess dB = new DBAccess();
            connection = dB.getConnection();
        }

        public SqlDataReader selectAll<T>(T table)
        { 
            string tableName = InfoClass.getClassName(table);
            return ReadInDataBase("SELECT * FROM " + tableName.ToUpper());
        }
        
        public SqlDataReader selectWithID<T>(T table, int id)
        {
            SqlParameter paramater = InfoClass.getId(table);
            string tableName = InfoClass.getClassName(table);
            return ReadInDataBase("SELECT * FROM " + tableName.ToUpper() + " WHERE " + paramater.ParameterName + "=" + paramater.Value);
        }

        public SqlDataReader selectBorrowedKeys()
        {
            return ReadInDataBase("SELECT k.idKey, keyName, p.idPerson, firstName, surename, beginDate, endDate FROM KEYS k " +
                "JOIN KEY_TYPE t ON t.idKeyType=k.idKeyType" +
                "JOIN KEY_BORROWED b ON b.idKey=k.idKey" +
                "JOIN PERSON p ON p.idPerson=b.idPerson");
        }

        public SqlDataReader selectAvailableKeys()
        {
            return ReadInDataBase("SELECT idKey, keyName, keyType FROM KEYS k" +
                "JOIN KEY_TYPE t ON t.idKeyType=k.idKeyType" +
                "WHERE idKey NOT IN (SELECT idKey FROM KEY_BORROWED)");
        }

        public void insert<T>(T table)
        {
            string tableName = InfoClass.getClassName(table);
            StringBuilder sb = new StringBuilder("Insert into " + tableName );
            SqlParameter[] attributeNames = InfoClass.getPropertyNames(table);
            int tableLength = attributeNames.Length - 1;
            //column where we need to add values
            for (int i = 1; i < tableLength; i++)
            {
                sb.Append(attributeNames[i].ParameterName + ", ");
            }
            sb.Append(attributeNames[tableLength].ParameterName + ")");
            //values of the columns
            sb.Append(" VALUES (");
            for (int i = 1; i < tableLength; i++)
            {
                sb.Append("@" + attributeNames[i].ParameterName + ", ");
            }
            sb.Append("@" + attributeNames[tableLength].ParameterName + ")");

            WriteInDatabase(sb.ToString(), attributeNames);
        }

        public void update<T>(T table)
        {
            string tableName = InfoClass.getClassName(table);
            StringBuilder sb = new StringBuilder("UPDATE " + tableName + " SET ");
            SqlParameter[] attributeNames = InfoClass.getPropertyNames(table);
            int tableLength = attributeNames.Length - 1;
            //We don't take the class id
            for (int i = 1; i < tableLength; i++)
            {
                sb.Append(attributeNames[i].ParameterName + "=" + "@" + attributeNames[i].ParameterName + ", ");
            }
            sb.Append(attributeNames[tableLength].ParameterName + "=" + "@" + attributeNames[tableLength].ParameterName);
            sb.Append(" WHERE " + attributeNames[0].ParameterName + "=" + attributeNames[0].Value);
            WriteInDatabase(sb.ToString(), attributeNames);
        }

        public void deleteWithId<T>(T table)
        {
            SqlParameter paramater = InfoClass.getId(table);
            string tableName = InfoClass.getClassName(table);
            ReadInDataBase("Delete FROM " + tableName.ToUpper() + " WHERE " + paramater.ParameterName + "=" + paramater.Value);
        }

        public void WriteInDatabase(string query, SqlParameter[] attributeNames)
        {
            try
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    if (attributeNames != null)
                    {
                        int nbrOfParam = attributeNames.Length;
                        for (int i = 1; i < nbrOfParam; i++)
                        {
                            command.Parameters.Add(attributeNames[i]);
                        }
                    }
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " = rows affected.");
                    connection.Close();
                }
            } catch(SqlException e)
            {
                Console.WriteLine("Can't write in the database : " + e.Source);
            }
            
        }

        public SqlDataReader ReadInDataBase(String query)
        {
            SqlDataReader reader = null;
            try
            {  
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    Console.WriteLine(query);
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetInt32(0),
                                    reader.GetString(1));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                    }
                    connection.Close();
                }
            } catch(SqlException e)
            {
                Console.WriteLine("Can't read in the database : " + e.Source);
            }
            return reader;
        }

    }
}
