using db_access;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager
{
    //This class will be used by the User Interface to make calls to the Manager so it should be populated only by functions the Manager performs for the UI
    public static class Manager
    {
        public static Key GetKey(int ID)
        {
            Console.WriteLine();
            return DBAccess.GetKeys()[ID];
        }

    }
}
