using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using SecureUWPChannel.Prooperties;

namespace SecureUWPChannel.Hmac
{
    public class MacAlgProvider
    {

        public static String CreateHMAC(String encrypted, String keyBytes)
        {

            String strAlgName = SampleConfiguration.MacAlg;
            MacAlgorithmProvider objMacProv = MacAlgorithmProvider.OpenAlgorithm(strAlgName);

            IBuffer buffKeyMaterial = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(keyBytes));
            IBuffer bufMsg = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(encrypted));

            CryptographicKey hmacKey = objMacProv.CreateKey(buffKeyMaterial);
            IBuffer buffHMAC = CryptographicEngine.Sign(hmacKey, bufMsg);

            // Verify that the HMAC length is correct for the selected algorithm
            if (buffHMAC.Length != objMacProv.MacLength)
            {
                throw new Exception("Error computing digest");
            }

            return CryptographicBuffer.EncodeToBase64String(buffHMAC);
        }

        public static bool VerifyHMAC(String encrypted, String keyBytes, String HmacMsg)
        {
            String ServerHmacSign = CreateHMAC(encrypted, keyBytes);
            if (HmacMsg.Equals(ServerHmacSign))
            {
                Debug.WriteLine("Integrity of Symetrickey verified successfully");
                return true;
            }
            else {
                Debug.WriteLine("Integrity of Symetrickey can not be verified");
                Debug.WriteLine(HmacMsg);
                Debug.WriteLine(ServerHmacSign);
                return false;
            }
        }


    }
}
