package Handshake;

public interface IOCallback {

    void SendDHEncryptedMessage(String Message);

    String ReceiveDHEncryptedMessage();
}
