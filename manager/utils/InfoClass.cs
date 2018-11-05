using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.SqlClient;

namespace manager
{
    public static class InfoClass
    {

        public static string getIdPropertyName<T>(T table)
        {
            PropertyInfo[] properties = table.GetType().GetProperties();
            return properties[0].Name;
        }

        public static string getClassName<T>(T aimClass) 
        {
            return aimClass.GetType().Name;
        }

        public static SqlParameter[] getPropertyNames<T>(T table)
        {
            PropertyInfo[] properties = table.GetType().GetProperties();
            int propLength = properties.Length;
            SqlParameter[] result = new SqlParameter[propLength];
            for(int i = 0; i < propLength; i++) 
            {
                if(properties[i].GetValue(table, null) != null)
                result[i] = new SqlParameter(properties[i].Name, properties[i].GetValue(table, null));
            }
            return result;
        }

        public static SqlParameter getId<T>(T table)
        {
            PropertyInfo[] properties = table.GetType().GetProperties();
            return new SqlParameter(properties[0].Name, properties[0].GetValue(table, null));
        }
    }
}
