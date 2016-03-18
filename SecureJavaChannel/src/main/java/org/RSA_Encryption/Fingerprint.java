package org.RSA_Encryption;

import Properties.Properties;
import static Properties.Properties.PRIVATE_KEY_FILE;
import java.io.IOException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.Signature;
import java.security.SignatureException;
import org.apache.commons.codec.binary.Base64;

public class Fingerprint {

    public static String siganture(String encrypted) throws NoSuchAlgorithmException, SignatureException, InvalidKeyException, NoSuchProviderException, IOException {

        Signature mySign = Signature.getInstance(Properties.Signature);

        mySign.initSign(IdentityKeyPair.readPrivateKeyFromFile(Properties.PRIVATE_KEY_FILE));
        mySign.update(encrypted.getBytes(Properties.CHAR_ENCODING));

        byte[] byteSignedData = mySign.sign();

        String encryptedString = new String(Base64.encodeBase64(byteSignedData));
        return encryptedString;
    }
}
