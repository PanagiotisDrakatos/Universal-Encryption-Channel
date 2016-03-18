using System;
using System.Diagnostics;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace EncryptionChannel.Cryptography
{
    public class RsaEncryption
    {
        public static string RsaEncrypt(String plainText, string publicKeyString)
        {
            try
            {

                // The next line fails with ASN1 bad tag value met
                IBuffer keyBuffer = CryptographicBuffer.DecodeFromBase64String(publicKeyString);
                AsymmetricKeyAlgorithmProvider provider = AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithmNames.RsaPkcs1);

                CryptographicKey publicKey = provider.ImportPublicKey(keyBuffer, CryptographicPublicKeyBlobType.Pkcs1RsaPublicKey);

                IBuffer dataBuffer = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(plainText));

                var encryptedData = CryptographicEngine.Encrypt(publicKey, dataBuffer, null);
                return CryptographicBuffer.EncodeToBase64String(encryptedData);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "Error in Encryption:With RSA ";
            }
        }
    }
}
