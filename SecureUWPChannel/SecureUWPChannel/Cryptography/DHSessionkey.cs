using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUWPChannel.Cryptography
{
    public class DHSessionkey
    {
        private String Sessionkey;

        public String SessionKey
        {
            get { return Sessionkey; }
            set { Sessionkey = value; }//set get methods to retrieve DΗSessionKey
        }

        public DHSessionkey() {
            this.Sessionkey = "";
        }

        public DHSessionkey(String Sessionkey) {
            this.Sessionkey= Sessionkey;
        }
    }
}
