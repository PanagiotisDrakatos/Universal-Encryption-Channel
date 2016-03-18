package Handshake;

import Properties.JSonObject;
import com.google.gson.Gson;
import org.json.JSONException;
import org.json.JSONObject;

public class JSonParse {

    public static final JSonObject ReadObject(String mystring) {
        JSonObject Readobj;
        Gson gson = new Gson();
        Readobj = gson.fromJson(mystring, JSonObject.class);
        return Readobj;

    }

    public static final String WriteObject(JSonObject Writeobj) throws JSONException {
        Gson gson = new Gson();
        JSONObject obj;
        obj = new JSONObject(gson.toJson(Writeobj));
        return obj.toString();
    }

}
