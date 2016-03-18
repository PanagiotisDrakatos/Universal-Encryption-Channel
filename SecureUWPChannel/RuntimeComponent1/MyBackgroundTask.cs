

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.Sockets;
using Windows.Security.Cryptography.Core;
using Windows.UI.Notifications;

namespace RuntimeComponent1
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {

        public void Run(IBackgroundTaskInstance taskInstance)
        {
          //   new IOSessionBuilder();
            SendToast("Hi kourdos");
            BigInteger S;
             private StreamSocket socket;
            SymmetricKeyAlgorithmProvider SAP;
            await Task.Delay(222);
        }

        public static void SendToast(string message)
        {
            
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);
            var elements = xml.GetElementsByTagName("text");
            var text = xml.CreateTextNode(message);

            elements[0].AppendChild(text);
            var toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
