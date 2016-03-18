package IOSocket;

import Properties.Properties;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.net.SocketException;

public class IOTransport {

    private final Socket socket;
    private InputStream is;
    private OutputStream os;
    private byte[] toSendLenBytes;

    public IOTransport(Socket socket) throws SocketException, IOException {
        this.socket = socket;
        //  this.socket.setSoTimeout(Properties.timeout);
        set_outpuStreams();
    }

    private void set_outpuStreams() throws IOException {
        is = socket.getInputStream();
        os = socket.getOutputStream();
    }

    public void SendMessage(String toSend) throws IOException {
        byte[] toSendBytes = toSend.getBytes();
        int toSendLen = toSendBytes.length;
        toSendLenBytes = new byte[4];
        CreateLenBytes(toSendLen);
        os.write(toSendLenBytes);
        os.write(toSendBytes);
    }

    public String receiveMessage() throws IOException {
        byte[] lenBytes = new byte[4];
        is.read(lenBytes, 0, 4);
        int len = ReadteLenBytes(lenBytes);
        byte[] receivedBytes = new byte[len];
        is.read(receivedBytes, 0, len);
        String received = new String(receivedBytes, 0, len);
        return received;
    }

    private void CreateLenBytes(int toSendLen) {
        toSendLenBytes[0]
                = (byte) (toSendLen & 0xff);
        toSendLenBytes[1]
                = (byte) ((toSendLen >> 8) & 0xff);
        toSendLenBytes[2]
                = (byte) ((toSendLen >> 16) & 0xff);
        toSendLenBytes[3]
                = (byte) ((toSendLen >> 24) & 0xff);
    }

    private int ReadteLenBytes(byte[] lenBytes) {
        int len;
        len = (((lenBytes[3] & 0xff) << 24)
                | ((lenBytes[2] & 0xff) << 16)
                | ((lenBytes[1] & 0xff) << 8)
                | (lenBytes[0] & 0xff));

        return len;
    }

    public void close() throws IOException {
        is.close();
        os.close();
        socket.close();
    }

}
