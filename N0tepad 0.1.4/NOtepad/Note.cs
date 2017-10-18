using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOtepad
{
    class Note
    {
        public struct date
        {
            public Int32 Day;
            public Int32 Month;
            public Int32 Year;
            public Int32 Hour;
            public Int32 Minute;
        }
        public date[] Date = new date[100];
        public String[] Title = new String[100];
        public String[] Content = new String[100];

    }
}
