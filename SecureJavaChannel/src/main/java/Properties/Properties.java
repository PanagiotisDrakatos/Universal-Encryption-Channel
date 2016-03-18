package Properties;

public class Properties {

    //socket properties
    public static final int ConnectionPort = 5555;
    public static final int MaxConnections = 100;
    public static final int timeout = 10 * 1000;

    public static final String END_PROTOCOL = "EndSession";
    //Put your message which you want taken from Server
   public static final String PlainText_UTF8 = "Hello Client Send me again Enrypted Message";
    
    //encryption properties  
    public static String DefaultCAkey = "s/L6D2CgMQ+qDKCJ6FjZ/w==";//key from Trusted ca

    //for more info check https://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange
    //g^x mod p 
    //However, its very unlikely that anyone else listening on the channel 
    //can calculate the key, since the calculation of discrete logarithms under 
    //field arithmetic is very hard (see Galois Fields)
    //Prime numbers machine generator 
    public static final String exponent = "95632573769194905177488615436919317766582673020891665265323677789504596581977";
    public static final String modulus = "81554351438297688582888558141846154981885664956959015742153749206820791432251";

    public static final String AES_PROVIDER = "AES";
    public static final String AES_ALGORITHM = "AES/CBC/PKCS7Padding";

    //Rsa needed
    public static final String RSA_ALGORITHM = "RSA";
    public static final String RSA_CRYPTO_ALGORITHM = "RSA/ECB/PKCS1Padding";
    public static final String RSA_Provider = "BC";

    //store_keys
    public static final String PUBLIC_KEY_FILE = "Public.key";
    public static final String PRIVATE_KEY_FILE = "Private.key";

    //Encodes values
    public static final String CHAR_ENCODING = "UTF-8";
    public static final String HashingAlgorithm = "md5";

    //keysizes-length
    public static final int KeySizeLength = 16;
    public static final int RSA_KEYSIZE = 1024;
    
     //HmacAlgProvider hash Function
     public static final String HmacAlgProv="HmacSHA256";
    //Rsa Integrity Signature
     public static final String Signature="SHA256withRSA";
}
