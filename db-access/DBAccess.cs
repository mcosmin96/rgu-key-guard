using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace db_access
{
    //This class will be used by the Manager to make calls to the database so it should be populated only by functions the Manager needs to know about
    public static class DBAccess
    {
        //Pull all keys from database operation (currently returning a mock key array)
        public static Key[] GetKeys()
        {
            return new Key[]
            {
                new Key()
                {
                    Code = "K_0120",
                    Type = KeyType.Key
                },
                new Key()
                {
                    Code = "K_0121",
                    Type = KeyType.Key
                },
                new Key()
                {
                    Code = "K_0122",
                    Type = KeyType.Key
                },
            };
        }
    }
}
