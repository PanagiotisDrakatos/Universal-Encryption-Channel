package Handshake;

import IOSocket.IOTransport;
import Properties.JSonObject;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.AES_Encryption.AES_Encryption;
import org.AES_Encryption.HMacAlgoProvider;
import org.AES_Encryption.IntegrityException;
import org.AES_Encryption.SessionKey;
import org.RSA_Encryption.Fingerprint;

public class IOMessageExhange implements IOCallback {

    private final IOTransport socketChanel;
    private final SessionKey Sessionkey;

    public IOMessageExhange(IOTransport socketChanel,
            SessionKey Sessionkey) {
        this.socketChanel = socketChanel;
        this.Sessionkey = Sessionkey;
    }

    @Override
    public void SendDHEncryptedMessage(String Message) {
        try {
            String EncryptedMessage = AES_Encryption.encrypt(Message, Sessionkey.getSessionKey());

            //Create Hmac Hash and send to Client to verify Integrity of symmetricKey
            String HmacHash = HMacAlgoProvider.HmacSha256Sign(EncryptedMessage, Sessionkey.getSessionKey());
            String signature = Fingerprint.siganture(EncryptedMessage);

            JSonObject ObjToSend = new JSonObject();
            ObjToSend.setEncryptedMessage(EncryptedMessage);
            ObjToSend.setHmacHash(HmacHash);
            ObjToSend.setFingerPrint(signature);

            socketChanel.SendMessage(JSonParse.WriteObject(ObjToSend));
        } catch (Exception ex) {
            Logger.getLogger(IOMessageExhange.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public String ReceiveDHEncryptedMessage() {

        try {
            JSonObject receivedObj = JSonParse.ReadObject(socketChanel.receiveMessage());

            if (!HMacAlgoProvider.HmacSha256Verify(receivedObj.getEncryptedMessage(),
                    Sessionkey.getSessionKey(), receivedObj.getHmacHash())) {
                throw new IntegrityException("Integrity of SymmetricKey canot verified");
            }
            //decrypt the message with DH key
            String DecryptedMessage = AES_Encryption.Decrypt(receivedObj.getEncryptedMessage(), Sessionkey.getSessionKey());

            return DecryptedMessage;
        } catch (Exception ex) {
            Logger.getLogger(IOMessageExhange.class.getName()).log(Level.SEVERE, null, ex);
            try {
                socketChanel.close();
            } catch (IOException ex1) {
                Logger.getLogger(IOMessageExhange.class.getName()).log(Level.SEVERE, null, ex1);
            }
        }
        return "Error in DH decrypt";

    }

}
