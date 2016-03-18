package Handshake;

public interface IOSynAck {

    void SendPublicKey();

    String ReceivePrimeNumber();

    void EndDHsession();

}
