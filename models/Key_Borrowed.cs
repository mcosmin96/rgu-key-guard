using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager;

namespace models
{
    public class Key_Borrowed
    {
        public int idBorrowed { get; set; }
        public int idKey { get; set; }
        public int idPerson { get; set; }
        public Date beginDate { get; set; }
        public Date endDate { get; set; }
        public string location { get; set; }
    }
}
