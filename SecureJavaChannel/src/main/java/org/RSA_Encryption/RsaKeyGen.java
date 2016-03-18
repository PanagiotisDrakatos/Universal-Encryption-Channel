package org.RSA_Encryption;

import Properties.Properties;
import java.io.IOException;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.Security;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.RSAPrivateKeySpec;
import java.security.spec.RSAPublicKeySpec;

public class RsaKeyGen {

    private PublicKey publicKey;
    private PrivateKey privateKey;

    public RsaKeyGen() throws NoSuchAlgorithmException, NoSuchProviderException, InvalidKeySpecException, IOException {
        int a, b;
        Security.addProvider(new org.bouncycastle.jce.provider.BouncyCastleProvider());
        boolean result = (IdentityKeyPair.Key_Files()) ? GenerateKeys() : false;
    }

    private boolean GenerateKeys() throws NoSuchAlgorithmException, NoSuchProviderException, InvalidKeySpecException, IOException {
        KeyPairGenerator keyPairGenerator = KeyPairGenerator.getInstance(Properties.RSA_ALGORITHM, Properties.RSA_Provider);
        keyPairGenerator.initialize(Properties.RSA_KEYSIZE); //2048 used for normal securities
        KeyPair keyPair = keyPairGenerator.generateKeyPair();
        publicKey = keyPair.getPublic();
        privateKey = keyPair.getPrivate();
        PullingParametrs();
        return true;
    }

    private void PullingParametrs() throws NoSuchAlgorithmException, InvalidKeySpecException, IOException {
        KeyFactory keyFactory = KeyFactory.getInstance(Properties.RSA_ALGORITHM);
        RSAPublicKeySpec rsaPubKeySpec = keyFactory.getKeySpec(publicKey, RSAPublicKeySpec.class);
        RSAPrivateKeySpec rsaPrivKeySpec = keyFactory.getKeySpec(privateKey, RSAPrivateKeySpec.class);
        IdentityKeyPair.SaveKeys(Properties.PUBLIC_KEY_FILE, rsaPubKeySpec.getModulus(), rsaPubKeySpec.getPublicExponent());
        IdentityKeyPair.SaveKeys(Properties.PRIVATE_KEY_FILE, rsaPrivKeySpec.getModulus(), rsaPrivKeySpec.getPrivateExponent());
    }

}
