using System;
using System.Threading.Tasks;
using SecureUWPChannel.Interfaces;
using SecureUWPChannel.Serialization;
using SecureUWPChannel.Prooperties;
using SecureUWPChannel.Cryptography;
using System.Numerics;
using SecureUWPChannel.Hmac;

namespace SecureUWPChannel.IOTransport
{
    public class DHkeyExchange : IAsyncSynAck
    {
        private IOMulticastAndBroadcast ActivitySocket;
        private JSonObject ReadObj;
        private JSonObject WriteObj;

        private SymmetricKeyGenerator SymmetricKey; 
        private PrimeNumberGenerator GeneratoreMachine;

        public DHkeyExchange(IOMulticastAndBroadcast ActivitySocket)
        {
            this.ActivitySocket = ActivitySocket;
            Intialize();
        }
        public async Task<String> GetPublicKey()
        {
            String readString = await ActivitySocket.read();
            ReadObj = JsonParse.ReadObject(readString);
            return ReadObj.getRSAPublicKey();
        } 

        public async Task StartDHSession()
        {
            
            String EncryptedClientNumber=AesEncryption.AES_Encrypt(GeneratoreMachine.GetClientPublicNumber(),
                SymmetricKey.StrKeyBase64);

           String HmacHash=MacAlgProvider.CreateHMAC(EncryptedClientNumber, SymmetricKey.StrKeyBase64);

            WriteObj.setClientEncryptedPrimeNumber(EncryptedClientNumber);
            WriteObj.setHmacHash(HmacHash);



            WriteObj.setEncryptedSymetricClientKey(RsaEncryption.RsaEncrypt(
                SymmetricKey.StrKeyBase64,
                ReadObj.getRSAPublicKey())
                );
                

            String StringTOsentWithRsa = JsonParse.WriteObject(WriteObj);

            await ActivitySocket.send(StringTOsentWithRsa);

        }

        public async Task<String> RetrieveDHSessionKey(String PublickKey)
        {
            String DecryptedServerNumber;
            String readString = await ActivitySocket.read();
            ReadObj = JsonParse.ReadObject(readString);
            if (FingerPrint.Verification(ReadObj.getServerPrimeNumber(), PublickKey,ReadObj.getFingerPrint()))
            {

                if (MacAlgProvider.VerifyHMAC(ReadObj.getServerPrimeNumber(),SymmetricKey.StrKeyBase64,
                    ReadObj.getHmacHash()))
                {
                    DecryptedServerNumber = AesEncryption.AES_Decrypt(ReadObj.getServerPrimeNumber(),
                        SymmetricKey.StrKeyBase64);
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

         return GeneratoreMachine.SessionDHGenerator(DecryptedServerNumber);
        }


       
        public void Intialize()
        {
            ReadObj = new JSonObject();
            WriteObj = new JSonObject();
            SymmetricKey = new SymmetricKeyGenerator();
            GeneratoreMachine = new PrimeNumberGenerator();
        }

    }
}
