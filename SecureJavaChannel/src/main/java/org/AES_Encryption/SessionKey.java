package org.AES_Encryption;

import Properties.Keystore;

public class SessionKey extends Keystore {

    private String SessionKey;

    public SessionKey() {
        super(Type.SessionKey);
        System.out.println(this.toString() + " created!");
    }

    public SessionKey(String SessionKey) {
        super(Type.SessionKey);
        this.SessionKey = SessionKey;
        System.out.println(this.toString() + " created!");
    }

    public void setSessionKey(String SessionKey) {
        this.SessionKey = SessionKey;
    }

    public String getSessionKey() {
        return SessionKey;
    }

    @Override
    public String toString() {
        return super.toString(); //To change body of generated methods, choose Tools | Templates.
    }

}
