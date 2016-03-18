package Handshake;

import IOSocket.IOTransport;
import Properties.RandomGenerator;
import Properties.JSonObject;
import Properties.Properties;
import java.io.IOException;
import java.math.BigInteger;
import java.net.SocketException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.AES_Encryption.AES_Encryption;
import org.AES_Encryption.ClientKey;
import org.AES_Encryption.HMacAlgoProvider;
import org.AES_Encryption.IntegrityException;
import org.AES_Encryption.SessionKey;
import org.RSA_Encryption.Fingerprint;
import org.RSA_Encryption.IdentityKeyPair;
import org.RSA_Encryption.RSA_Encryption;

public final class DHkeyExchange implements IOSynAck {

    private static IOMessageExhange Builder;
    private static RandomGenerator Genarator;

    private final IOTransport socketChanel;
    private final SessionKey Sessionkey;
    private ClientKey ClientKey;

    public DHkeyExchange(IOTransport socketChanel, SessionKey Sessionkey) throws SocketException {
        this.socketChanel = socketChanel;
        this.Sessionkey = Sessionkey;
        Inintialize();
    }

    //More readable is more efficient. Temporary expressions and local variables need the same space and 
    //from CPU/JVM perspective it doesn't make much difference. JVM will do a better job 
    //optimizing/inling it
    @Override
    public void SendPublicKey() {
        try {
            //send PublicKey to client
            JSonObject ObjToSend = new JSonObject();
            ObjToSend .setRSAPublicKey(IdentityKeyPair.PublicKeyString());
            
            String toSend = JSonParse.WriteObject(ObjToSend);
            System.out.println(toSend);
            socketChanel.SendMessage(toSend);

            System.out.println("Server----------------(publicKey)---------->Client");
        } catch (IOException ex) {
            Logger.getLogger(DHkeyExchange.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public String ReceivePrimeNumber() {
        try {
            //Receive JSObject from Client
            JSonObject receivedObj = JSonParse.ReadObject(socketChanel.receiveMessage());

            //Decrypts ClientSymmetrickey with server private Rsa and store the result to client key
            ClientKey.setClientKey(RSA_Encryption.RsaDecrypt(
                    IdentityKeyPair.readPrivateKeyFromFile(Properties.PRIVATE_KEY_FILE),
                    receivedObj.getEncryptedSymetricClientKey()));

            System.out.println("Client symmetricKey :" + ClientKey.getClientKey());

            //Check the Integrity of EncryptedPrimeNumber with clients Symmetrickey
            if (!HMacAlgoProvider.HmacSha256Verify(receivedObj.getClientEncryptedPrimeNumber(),
                    ClientKey.getClientKey(), receivedObj.getHmacHash())) {
                throw new IntegrityException("Integrity of SymmetricKey canot verified");
            }
            //Decrypt ClientPrimeNymber With ClientSymmetricKey
            String DecryptedAESprimeNumber = AES_Encryption.Decrypt(receivedObj.getClientEncryptedPrimeNumber(),
                    ClientKey.getClientKey());

            //Generate DHkey and store it to Sessionkey
            BigInteger sessionResult = Genarator.SessionGenerator(DecryptedAESprimeNumber);
            Sessionkey.setSessionKey(sessionResult.toString());

            System.out.println("Server <------PublicKeyEncrypted(CipherEncrypted(ClientKeyEncrypted(publicPrimeNumber)),ClientKey)---------------Client");
            return Sessionkey.getSessionKey();
        } catch (Exception ex) {
            Logger.getLogger(DHkeyExchange.class.getName()).log(Level.SEVERE, null, ex);

        }
        return "Problem Get DHSessionKey";
    }

    @Override
    public void EndDHsession() {

        try {
            //generate Server public Prime Number
            BigInteger ServerPublicPrimeNumber = Genarator.GeneratePublicPrimeNumber();

            //Encryptys Server public Prime Number with client Key
            String EncryptedAESprimeNumber = AES_Encryption.encrypt(ServerPublicPrimeNumber.toString(),
                    ClientKey.getClientKey());
            System.out.println("SesionKey " + Sessionkey.getSessionKey());
            //Convert The encrypted to Json Format
            JSonObject ObjToSend = new JSonObject();

            //Create Hmac Hash and send to Client to verify Integrity of symmetricKey
            String HmacHash = HMacAlgoProvider.HmacSha256Sign(EncryptedAESprimeNumber, ClientKey.getClientKey());

            //Server sign the encrypted message with private key 
            String signature = Fingerprint.siganture(EncryptedAESprimeNumber);

            ObjToSend.setServerPrimeNumber(EncryptedAESprimeNumber);
            ObjToSend.setHmacHash(HmacHash);
            ObjToSend.setFingerPrint(signature);

            String JsonStringToEncrypt = JSonParse.WriteObject(ObjToSend);
            socketChanel.SendMessage(JsonStringToEncrypt);
        } catch (Exception ex) {
            Logger.getLogger(DHkeyExchange.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void Inintialize() {
        Genarator = new RandomGenerator();
        ClientKey = new ClientKey();
    }

}
