package org.AES_Encryption;

import Properties.Properties;
import java.security.Security;
import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import org.apache.commons.codec.binary.Base64;

public class AES_Encryption {

    public AES_Encryption() {
        Security.addProvider(new org.bouncycastle.jce.provider.BouncyCastleProvider());
    }

    public static String encrypt(String plainText, String key) throws Exception {

        Security.addProvider(new org.bouncycastle.jce.provider.BouncyCastleProvider());

        byte[] keyBytes = Diggest.Diggest(key, Properties.HashingAlgorithm);
        // Use the first 16 bytes (or even less if key is shorter)
        byte[] keyBytes16 = new byte[Properties.KeySizeLength];
        System.arraycopy(keyBytes, 0, keyBytes16, 0, Math.min(keyBytes.length, Properties.KeySizeLength));

        // convert plain text to bytes
        byte[] plainBytes = plainText.getBytes(Properties.CHAR_ENCODING);

        SecretKeySpec skeySpec = new SecretKeySpec(keyBytes16, Properties.AES_PROVIDER);
        Cipher cipher = Cipher.getInstance(Properties.AES_ALGORITHM);
        byte[] iv = new byte[16]; // initialization vector with all 0 just like openssll
        cipher.init(Cipher.ENCRYPT_MODE, skeySpec, new IvParameterSpec(iv));

        // encrypt
        byte[] encrypted = cipher.doFinal(plainBytes);
        String encryptedString = new String(Base64.encodeBase64(encrypted));

        return encryptedString;
    }

    public static String Decrypt(String EncryptedText, String key) throws Exception {

        Security.addProvider(new org.bouncycastle.jce.provider.BouncyCastleProvider());

        byte[] keyBytes = Diggest.Diggest(key, Properties.HashingAlgorithm);
        // Use the first 16 bytes (or even less if key is shorter)
        byte[] keyBytes16 = new byte[Properties.KeySizeLength];

        System.arraycopy(keyBytes, 0, keyBytes16, 0, Math.min(keyBytes.length, Properties.KeySizeLength));

        // convert plain text to bytes
        byte[] plainBytes = Base64.decodeBase64(EncryptedText.getBytes(Properties.CHAR_ENCODING));
        SecretKeySpec skeySpec = new SecretKeySpec(keyBytes16, Properties.AES_PROVIDER);
        Cipher cipher = Cipher.getInstance(Properties.AES_ALGORITHM);
        byte[] iv = new byte[16]; // initialization vector with all 0
        cipher.init(Cipher.DECRYPT_MODE, skeySpec, new IvParameterSpec(iv));

        byte[] decrypteed = cipher.doFinal(plainBytes);

        return new String(decrypteed, Properties.CHAR_ENCODING);
    }
}
