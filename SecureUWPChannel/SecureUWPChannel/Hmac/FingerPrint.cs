using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace SecureUWPChannel.Hmac
{
    public class FingerPrint
    {

        public static bool Verification(String Encrypted, String Publickey, String Hmacs)
        {
            IBuffer buffMsg = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(Encrypted));
            IBuffer keyBuffer = CryptographicBuffer.DecodeFromBase64String(Publickey);
            IBuffer buffHMAC = CryptographicBuffer.DecodeFromBase64String(Hmacs);

            AsymmetricKeyAlgorithmProvider provider = AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithmNames.RsaSignPkcs1Sha256);

            CryptographicKey publicKey = provider.ImportPublicKey(keyBuffer, CryptographicPublicKeyBlobType.Pkcs1RsaPublicKey);

            if (!SampleVerifyHMAC(buffMsg, publicKey, buffHMAC)) { return false; }

            return true;
        }

        private static bool SampleVerifyHMAC(IBuffer buffMsg, CryptographicKey hmacKey, IBuffer buffHMAC)
        {

            Boolean IsAuthenticated = CryptographicEngine.VerifySignature(hmacKey, buffMsg, buffHMAC);
            if (!IsAuthenticated)
            {
                Debug.WriteLine("The Integrity of RSA cannot be verified.");
                return false;
            }
            else
            {
                Debug.WriteLine("The Integrity of RSA verified succesful");
                return true;
            }
        }
    }
}
