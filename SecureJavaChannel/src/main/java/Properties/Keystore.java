package Properties;

public abstract class Keystore {

    private final Type type;

    public enum Type {

        ClientKey, SessionKey
    }

    //this constructor  doesn't actually "BUILD" the object, it is used to initialize fields.
    public Keystore(Type type) {
        this.type = type;
    }

    public Boolean isType(Type type) {
        return (this.type == type);
    }

    @Override
    public String toString() {
        return type.toString();
    }
}
