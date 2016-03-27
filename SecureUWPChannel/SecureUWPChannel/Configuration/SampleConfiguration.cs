using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Security.Cryptography.Core;
using Windows.Storage;

namespace SecureUWPChannel.Prooperties
{
    public class SampleConfiguration
    {
        //socket properties
        public static  String ConnectionPort = "5555";
        public static  int MaxConnections = 100;
        public static  int timeout = 4000;
        public static String Host = "localhost";

        //encryption properties  
        public static String strSecret = "PutAStrongPassword";
        public static String MacAlg = MacAlgorithmNames.HmacSha256;

        //Put your Message to send to server
        public static String Messages = "Hello Server :D";
             
        //for more info check https://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange
        //g^x mod p 
        //However, its very unlikely that anyone else listening on the channel 
        //can calculate the key, since the calculation of discrete logarithms under 
        //field arithmetic is very hard (see Galois Fields)
        //Prime numbers machine generator 
        public static  String exponent = "95632573769194905177488615436919317766582673020891665265323677789504596581977";
        public static  String modulus = "81554351438297688582888558141846154981885664956959015742153749206820791432251";

        public const string SampleBackgroundTaskEntryPoint = "Tasks.SampleBackgroundTask";
        public const string SampleBackgroundTaskName = "SampleBackgroundTask";
        public static string SampleBackgroundTaskProgress = "";
        public static bool SampleBackgroundTaskRegistered = false;

      

        public const string TimeTriggeredTaskName = "TimeTriggeredTask";
        public static string TimeTriggeredTaskProgress = "";
        public static bool TimeTriggeredTaskRegistered = false;

        public const string ApplicationTriggerTaskName = "ApplicationTriggerTask";
        public static string ApplicationTriggerTaskProgress = "";
        public static string ApplicationTriggerTaskResult = "";
        public static bool ApplicationTriggerTaskRegistered = false;

     
        public static bool TaskRequiresBackgroundAccess(String name)
        {
            if ((name == TimeTriggeredTaskName) ||
                (name == ApplicationTriggerTaskName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
