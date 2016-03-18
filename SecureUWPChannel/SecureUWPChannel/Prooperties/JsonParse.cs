using Newtonsoft.Json;
using SecureUWPChannel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUWPChannel.Prooperties
{
    public class JsonParse
    {
        public static JSonObject ReadObject(String myString)
        {
            return JsonConvert.DeserializeObject<JSonObject>(myString);
        }

        public static String WriteObject(JSonObject WriteObj)
        {
            return JsonConvert.SerializeObject(WriteObj);

        }
    }
}
