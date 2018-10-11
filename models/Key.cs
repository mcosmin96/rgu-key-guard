using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class Key
    {
        public string Code { get; set; }
        public KeyType Type { get; set; }
    }

    public enum KeyType
    {
        Key,
        Fob
    }
}
