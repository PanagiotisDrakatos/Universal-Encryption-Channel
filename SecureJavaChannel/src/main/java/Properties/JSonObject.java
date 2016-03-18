/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Properties;

public class JSonObject {

    public String EncryptedSymetricClientKey;
    public String RSAPublicKey;

    public String ClientEncryptedPrimeNumber;
    public String ServerPrimeNumber;

    public String EncryptedMessage;
    public String fingerPrint;
    public String HmacHash;

    public JSonObject(String ClientEncryptedPrimeNumber, String EncryptedSymetricClientKey) {
        this.ClientEncryptedPrimeNumber = ClientEncryptedPrimeNumber;
        this.EncryptedSymetricClientKey = EncryptedSymetricClientKey;
        this.ServerPrimeNumber = "Nulls";
        this.RSAPublicKey = "Nulls";
        this.EncryptedMessage = "Nulls";
        this.fingerPrint = "Nulls";
        this.HmacHash = "Nulls";
    }

    public JSonObject(String RSAPublicKey) {
        this.RSAPublicKey = RSAPublicKey;
        this.EncryptedSymetricClientKey = "Nulls";
        this.ClientEncryptedPrimeNumber = "Nulls";
        this.ServerPrimeNumber = "Nulls";
        this.EncryptedMessage = "Nulls";
        this.fingerPrint = "Nulls";
        this.HmacHash = "Nulls";
    }

    public JSonObject() {
        this.EncryptedSymetricClientKey = "Nulls";
        this.ClientEncryptedPrimeNumber = "Nulls";
        this.ServerPrimeNumber = "Nulls";
        this.RSAPublicKey = "Nulls";
        this.EncryptedMessage = "Nulls";
        this.fingerPrint = "Nulls";
        this.HmacHash = "Nulls";
    }

    public void setEncryptedSymetricClientKey(String EncryptedSymetricClientKey) {
        this.EncryptedSymetricClientKey = EncryptedSymetricClientKey;
    }

    public String getEncryptedSymetricClientKey() {
        return EncryptedSymetricClientKey;
    }

    public void setServerPrimeNumber(String ServerPrimeNumber) {
        this.ServerPrimeNumber = ServerPrimeNumber;
    }

    public void setClientEncryptedPrimeNumber(String ClientEncryptedPrimeNumber) {
        this.ClientEncryptedPrimeNumber = ClientEncryptedPrimeNumber;
    }

    public void setClientKey(String EncryptedSymetricClientKey) {
        this.EncryptedSymetricClientKey = EncryptedSymetricClientKey;
    }

    public String getServerPrimeNumber() {
        return ServerPrimeNumber;
    }

    public String getClientEncryptedPrimeNumber() {
        return ClientEncryptedPrimeNumber;
    }

    public void setMessage(String Message) {
        this.EncryptedSymetricClientKey = Message;
    }

    public String getMessage() {
        return EncryptedSymetricClientKey;
    }

    public void setRSAPublicKey(String RSAPublicKey) {
        this.RSAPublicKey = RSAPublicKey;
    }

    public String getRSAPublicKey() {
        return RSAPublicKey;
    }

    public void setEncryptedMessage(String EncryptedMessage) {
        this.EncryptedMessage = EncryptedMessage;
    }

    public String getEncryptedMessage() {
        return EncryptedMessage;
    }

    public String getFingerPrint() {
        return fingerPrint;
    }

    public void setFingerPrint(String fingerPrint) {
        this.fingerPrint = fingerPrint;
    }

    public void setHmacHash(String HmacHash) {
        this.HmacHash = HmacHash;
    }

    public String getHmacHash() {
        return HmacHash;
    }

    public void Intialize() {
        this.EncryptedSymetricClientKey = "Nulls";
        this.ClientEncryptedPrimeNumber = "Nulls";
        this.ServerPrimeNumber = "Nulls";
        this.RSAPublicKey = "Nulls";
        this.EncryptedMessage = "Nulls";
        this.fingerPrint = "Nulls";
    }
}
