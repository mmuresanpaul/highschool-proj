using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOtepad
{
    class Account
    {
       // Note Notes = new Note();
        public struct AccountDetails
        {
            public String FirstName;
            public String LastName;
            public String Email;
            public String Password;
            public Note[] Note;
        }
        public AccountDetails[] Users = new AccountDetails[100];

        public Int32 AccountNumber = 0;
    }

}
