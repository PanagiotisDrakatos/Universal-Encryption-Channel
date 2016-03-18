package org.AES_Encryption;

public class IntegrityException extends Exception {

    public IntegrityException() {
        super();
    }

    public IntegrityException(String message) {
        super(message);
    }

    public IntegrityException(String message, Throwable cause) {
        super(message, cause);
    }

    public IntegrityException(Throwable cause) {
        super(cause);
    }
}
