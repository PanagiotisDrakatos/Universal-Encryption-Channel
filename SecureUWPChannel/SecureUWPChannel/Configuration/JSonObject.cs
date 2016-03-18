using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUWPChannel.Serialization
{


    public class JSonObject
    {

        public String EncryptedSymetricClientKey;
        public String RSAPublicKey;

        public String ClientEncryptedPrimeNumber;
        public String ServerPrimeNumber;

        public String EncryptedMessage;
        public String fingerPrint;
        public String HmacHash;

        public JSonObject()
        {
            Intialize();
        }

        public void setEncryptedSymetricClientKey(String EncryptedSymetricClientKey)
        {
            this.EncryptedSymetricClientKey = EncryptedSymetricClientKey;
        }

        public String getEncryptedSymetricClientKey()
        {
            return EncryptedSymetricClientKey;
        }

        public void setServerPrimeNumber(String ServerPrimeNumber)
        {
            this.ServerPrimeNumber = ServerPrimeNumber;
        }

        public void setClientEncryptedPrimeNumber(String ClientEncryptedPrimeNumber)
        {
            this.ClientEncryptedPrimeNumber = ClientEncryptedPrimeNumber;
        }

        public void setClientKey(String EncryptedSymetricClientKey)
        {
            this.EncryptedSymetricClientKey = EncryptedSymetricClientKey;
        }

        public String getServerPrimeNumber()
        {
            return ServerPrimeNumber;
        }

        public String getClientEncryptedPrimeNumber()
        {
            return ClientEncryptedPrimeNumber;
        }

        public void setMessage(String Message)
        {
            this.EncryptedSymetricClientKey = Message;
        }

        public String getMessage()
        {
            return EncryptedSymetricClientKey;
        }

        public void setRSAPublicKey(String RSAPublicKey)
        {
            this.RSAPublicKey = RSAPublicKey;
        }

        public String getRSAPublicKey()
        {
            return RSAPublicKey;
        }

        public void setEncryptedMessage(String EncryptedMessage)
        {
            this.EncryptedMessage = EncryptedMessage;
        }

        public String getEncryptedMessage()
        {
            return EncryptedMessage;
        }

        public String getFingerPrint()
        {
            return fingerPrint;
        }

        public void setFingerPrint(String fingerPrint)
        {
            this.fingerPrint = fingerPrint;
        }

        public void setHmacHash(String HmacHash)
        {
            this.HmacHash = HmacHash;
        }

        public String getHmacHash()
        {
            return HmacHash;
        }

        public void Intialize()
        {
            this.EncryptedSymetricClientKey = "";
            this.ClientEncryptedPrimeNumber = "";
            this.ServerPrimeNumber = "";
            this.RSAPublicKey = "";
            this.EncryptedMessage = "";
            this.fingerPrint = "";
        }
    }

}

