package IOSocket;

import Properties.Properties;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import org.RSA_Encryption.RsaKeyGen;

public final class EstablishConnection {

    private ServerSocket ServerSocket;
    private Socket UwpSocket;

    public EstablishConnection() throws Exception {
        RsaKeyGen rsaKeyGen = new RsaKeyGen(); //generate Servers RsaKeypair if no exists 
        AcceptClients();
    }

    public void AcceptClients() throws IOException, SocketIOException {

        ServerSocket = new ServerSocket(Properties.ConnectionPort, Properties.MaxConnections);
        System.out.println("Waiting for incoming C# Clients" + "\n");
        while (true) {
            try {
                UwpSocket = ServerSocket.accept();
                System.out.println("ESTABLISHED" + "\n");
                System.out.println("Just connected to " + UwpSocket.getInetAddress() + "\n");
                IOTransport socketChanel = new IOTransport(UwpSocket);
                new SessionHandler(socketChanel).start();
            } catch (Exception e) {
                UwpSocket.close();
                throw new SocketIOException();
            }
        }

    }

}
