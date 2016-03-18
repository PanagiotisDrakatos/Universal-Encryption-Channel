package org.AES_Encryption;

import Properties.Properties;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import org.apache.commons.codec.binary.Base64;

public class HMacAlgoProvider {

    public static String HmacSha256Sign(String Encrypted, String key) throws Exception {
        Mac sha256_HMAC = Mac.getInstance(Properties.HmacAlgProv);
        byte[] keybytes = key.getBytes(Properties.CHAR_ENCODING);
        SecretKeySpec secret_key = new SecretKeySpec(keybytes, Properties.HmacAlgProv);
        sha256_HMAC.init(secret_key);

        byte[] EncryptedBytes = Encrypted.getBytes(Properties.CHAR_ENCODING);
        String hash = new String(Base64.encodeBase64(sha256_HMAC.doFinal(EncryptedBytes)));
        return hash;
    }

    public static boolean HmacSha256Verify(String Encrypted, String key, String HmacMsg) throws Exception {

        String ServerHmacSign = HmacSha256Sign(Encrypted, key);
        if (HmacMsg.equals(ServerHmacSign)) {
            System.out.println("Integrity verified successfully");
            return true;
        } else {
            System.out.println("Integrity Of Message can not be verified");
            return false;
        }

    }
}
