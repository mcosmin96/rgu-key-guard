using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class Keys
    {
        public int idKey { get; set; }
        public string keyName { get; set; }
        public int idKeyType { get; set; }
        public int isLost { get; set; }
        public int isUnknown { get; set; }
    }
}
