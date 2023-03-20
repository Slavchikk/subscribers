using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscribers
{
   
    public partial class USERS
    {
        public string FIO
        {
            get
            {
                return "" + SURNAME + " " + FIRSTNAME + " " + LASTNAME;
            }
        }
    }
}
