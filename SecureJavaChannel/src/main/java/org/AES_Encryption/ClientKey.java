package org.AES_Encryption;

import Properties.Keystore;

public class ClientKey extends Keystore {

    private String ClientKey;

    public ClientKey() {
        super(Type.ClientKey);
        System.out.println(this.toString() + " created!");
    }

    public ClientKey(String ClientKey) {
        super(Type.ClientKey);
        this.ClientKey = ClientKey;
        System.out.println(this.toString() + " created!");
    }

    public String getClientKey() {
        return ClientKey;
    }

    public void setClientKey(String ClientKey) {
        this.ClientKey = ClientKey;
    }

    @Override
    public String toString() {
        return super.toString(); //To change body of generated methods, choose Tools | Templates.
    }

}
