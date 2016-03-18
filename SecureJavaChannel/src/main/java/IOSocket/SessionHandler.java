package IOSocket;

import Handshake.DHkeyExchange;
import Handshake.IOCallback;
import Handshake.IOMessageExhange;
import Handshake.IOSynAck;
import Properties.Properties;
import java.io.IOException;
import java.net.SocketException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.AES_Encryption.ClientKey;
import org.AES_Encryption.SessionKey;

public class SessionHandler extends Thread {

    private final IOTransport socketChanel;
    private final IOSynAck keyExchange;
    private final SessionKey Sessionkey;

    private IOCallback MessageExhange;


    public SessionHandler(IOTransport socketChanel) throws SocketException {
        this.socketChanel = socketChanel;
        this.Sessionkey = new SessionKey();
        this.keyExchange = new DHkeyExchange(this.socketChanel, this.Sessionkey);
    }

    @Override
    public synchronized void run() {

        try {
            EstablishDHSession();
            System.out.println("---------------DHSessionEstablished--------------------");
            Intialize();
            ContinueSession();
        } catch (Exception e) {
            System.out.println(e.toString());
        } finally {
            close();
        }
    }

    public void EstablishDHSession() {
        this.keyExchange.SendPublicKey();
        Sessionkey.setSessionKey(this.keyExchange.ReceivePrimeNumber());
        this.keyExchange.EndDHsession();
    }

    public void ContinueSession() {
        //Receive Message
        String Receive = this.MessageExhange.ReceiveDHEncryptedMessage();
        System.out.println("Client Says : " + Receive);

        //send Response 
        this.MessageExhange.SendDHEncryptedMessage(Properties.PlainText_UTF8);
    }

    private void Intialize() {
        this.MessageExhange = new IOMessageExhange(this.socketChanel, this.Sessionkey);
    }

    private void close() {
        try {
            this.socketChanel.close();
        } catch (IOException ex) {
            Logger.getLogger(SessionHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
