using SecureUWPChannel.Cryptography;
using SecureUWPChannel.Interfaces;
using SecureUWPChannel.Prooperties;
using SecureUWPChannel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureUWPChannel.Hmac;

namespace SecureUWPChannel.IOTransport
{
    public class IOHandlerDHMessage : IOCallbackAsync
    {
        private IOMulticastAndBroadcast ActivitySocket;
        private DHSessionkey sessionKey;

        private JSonObject ReadObj;
        private JSonObject WriteObj;


        public IOHandlerDHMessage(IOMulticastAndBroadcast ActivitySocket, DHSessionkey sessionKey)
        {
            this.ActivitySocket = ActivitySocket;
            this.sessionKey = sessionKey;
            Intialize();
        }
        public async Task<String> ReceiveDHEncryptedMessage(String PublicKey)
        {
            
            ReadObj = JsonParse.ReadObject(await ActivitySocket.read());

            if (FingerPrint.Verification(ReadObj.getEncryptedMessage(),PublicKey, ReadObj.getFingerPrint()))
            {
                if (MacAlgProvider.VerifyHMAC(ReadObj.getEncryptedMessage(),sessionKey.SessionKey,
                    ReadObj.getHmacHash()))
                {
                    return AesEncryption.AES_Decrypt(ReadObj.getEncryptedMessage(),
                        sessionKey.SessionKey);
                }
                else
                {
                    throw new Exception("Integrity of SymmetricKey canot verified");
                }
            }
            else
            {
                throw new Exception("Integrity of RSA canot verified");
            }
           
        }

        public async Task SendDHEncryptedMessage(String Message)
        {
            String encryptedMessage=(AesEncryption.AES_Encrypt(Message,sessionKey.SessionKey));
            String HmacHash = MacAlgProvider.CreateHMAC(encryptedMessage, sessionKey.SessionKey);

            WriteObj.setEncryptedMessage(encryptedMessage);
            WriteObj.setHmacHash(HmacHash);

            await ActivitySocket.send(JsonParse.WriteObject(WriteObj));
        }


        public void Intialize()
        {
            ReadObj = new JSonObject();
            WriteObj = new JSonObject();
        }
    }
}
