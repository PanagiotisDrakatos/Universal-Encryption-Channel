package org.RSA_Encryption;

import Properties.Properties;
import java.security.PrivateKey;
import java.security.PublicKey;
import javax.crypto.Cipher;
import org.apache.commons.codec.binary.Base64;

public class RSA_Encryption {

    public static String RsaEecrypt(PublicKey pubKey, String plainText) throws Exception {
        byte[] plainBytes = plainText.getBytes(Properties.CHAR_ENCODING);
        Cipher cipher = Cipher.getInstance(Properties.RSA_CRYPTO_ALGORITHM, Properties.RSA_Provider);

        byte[] iv = new byte[16];
        cipher.init(Cipher.ENCRYPT_MODE, pubKey);

        byte[] encrypted = cipher.doFinal(plainBytes);
        String encryptedString = new String(Base64.encodeBase64(encrypted));

        return encryptedString;
    }

    public static String RsaDecrypt(PrivateKey privateKey, String EncryptedText) throws Exception {
        byte[] plainBytes = Base64.decodeBase64(EncryptedText.getBytes(Properties.CHAR_ENCODING));
        byte[] iv = new byte[16]; // initialization vector with all 0

        Cipher cipher = Cipher.getInstance(Properties.RSA_CRYPTO_ALGORITHM, Properties.RSA_Provider);
        cipher.init(Cipher.DECRYPT_MODE, privateKey);

        byte[] decrypteed = cipher.doFinal(plainBytes);
        String DecryptedString = new String(decrypteed, "UTF-8");

        return DecryptedString;
    }
}
