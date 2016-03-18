package org.AES_Encryption;

import Properties.Properties;
import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Diggest {

    public static byte[] Diggest(String key, String algorithm) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        MessageDigest md = MessageDigest.getInstance(algorithm);
        md.update(key.getBytes(Properties.CHAR_ENCODING));
        byte[] keyBytes = md.digest();
        return keyBytes;
    }
}
