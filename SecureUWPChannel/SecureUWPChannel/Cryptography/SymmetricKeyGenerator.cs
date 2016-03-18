using SecureUWPChannel.IOTransport;
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

namespace SecureUWPChannel.Cryptography
{
    public class SymmetricKeyGenerator
    {
        private String strKeyBase64;

        public String StrKeyBase64
        {
            get { return strKeyBase64; }
            set { strKeyBase64 = value; }
        }



        public SymmetricKeyGenerator()
        {

            try {
                // Create a symmetric session key.
                String strSymmetricAlgName = KeyDerivationAlgorithmNames.Pbkdf2Sha256;
                UInt32 symmetricKeyLength = 32;
                this.strKeyBase64 = SampleDeriveFromPbkdf(strSymmetricAlgName, symmetricKeyLength);                              
            }catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

        }

        public String SampleDeriveFromPbkdf(
    String strAlgName,
    UInt32 targetSize)
        {
            // Open the specified algorithm.
            KeyDerivationAlgorithmProvider objKdfProv = KeyDerivationAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Create a buffer that contains the secret used during derivation.
           
            IBuffer buffSecret = CryptographicBuffer.ConvertStringToBinary(SampleConfiguration.strSecret, BinaryStringEncoding.Utf8);

            // Create a random salt value.
            IBuffer buffSalt = CryptographicBuffer.GenerateRandom(32);

            // Specify the number of iterations to be used during derivation.
            UInt32 iterationCount = 10000;

            // Create the derivation parameters.
            KeyDerivationParameters pbkdf2Params = KeyDerivationParameters.BuildForPbkdf2(buffSalt, iterationCount);

            // Create a key from the secret value.
            CryptographicKey keyOriginal = objKdfProv.CreateKey(buffSecret);

            // Derive a key based on the original key and the derivation parameters.
            IBuffer keyDerived = CryptographicEngine.DeriveKeyMaterial(
                keyOriginal,
                pbkdf2Params,
                targetSize);

            // Encode the key to a Base64 value (for display)
            String strKeyBase64 = CryptographicBuffer.EncodeToHexString(keyDerived);
            // Return the encoded string
            return strKeyBase64;
        }







    }
}
