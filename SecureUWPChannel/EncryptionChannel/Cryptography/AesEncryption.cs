using System;
using System.Diagnostics;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace EncryptionChannel.Cryptography
{
    public class AesEncryption
    {
        public static string AES_Encrypt(String plainText, String EncryptionKey)
        {
            string encrypted = "";
            SymmetricKeyAlgorithmProvider SAP = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            HashAlgorithmProvider HAP = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash Hash_AES = HAP.CreateHash();

            try
            {
                //byte[] KeyBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] KeyBytes16 = new byte[16];
                Hash_AES.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(EncryptionKey)));
                byte[] KeyBytes;
                CryptographicBuffer.CopyToByteArray(Hash_AES.GetValueAndReset(), out KeyBytes);
                for (int i = 0; i < KeyBytes.Length; i++)
                {
                    KeyBytes16[i] = KeyBytes[i];
                }

                CryptographicKey key = SAP.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(KeyBytes16));

                IBuffer Buffer = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(plainText));
                encrypted = CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(key, Buffer, null));

                return encrypted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "Error in Encryption:With Aes ";
            }
        }


        public static string AES_Decrypt(String EncryptedText, String DecryptionKey)
        {

            string decrypted = "";
            SymmetricKeyAlgorithmProvider SAP = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            HashAlgorithmProvider HAP = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash Hash_AES = HAP.CreateHash();

            try
            {
                //byte[] KeyBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] KeyBytes16 = new byte[16];
                Hash_AES.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(DecryptionKey)));
                byte[] KeyBytes;
                CryptographicBuffer.CopyToByteArray(Hash_AES.GetValueAndReset(), out KeyBytes);
                for (int i = 0; i < KeyBytes.Length; i++)
                {
                    KeyBytes16[i] = KeyBytes[i];
                }

                CryptographicKey key = SAP.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(KeyBytes16));


                IBuffer Buffer = CryptographicBuffer.DecodeFromBase64String(EncryptedText);
                byte[] Decrypted;

                CryptographicBuffer.CopyToByteArray(CryptographicEngine.Decrypt(key, Buffer, null), out Decrypted);
                decrypted = System.Text.Encoding.UTF8.GetString(Decrypted, 0, Decrypted.Length);
                return decrypted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "Error in Decryption:With Aes ";
            }
        }
    }
}
