using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using SecureUWPChannel.Interfaces;
using SecureUWPChannel.Serialization;
using SecureUWPChannel.Cryptography;
using Windows.UI.Popups;
using SecureUWPChannel.Prooperties;

namespace SecureUWPChannel.IOTransport
{
    public class IOSessionBuilder
    {
       // private Task Initialization;
        private StreamSocket CommunicationSocket;
        private DHSessionkey sessionKey;
        private String PublicKey;

        public IOSessionBuilder()
        {
            
            this.sessionKey = new DHSessionkey();
        }
        public async Task InitializeAsync()
        {
            using (CommunicationSocket = new StreamSocket())
            {

                try
                {
                    IOMulticastAndBroadcast Activity = new IOMulticastAndBroadcast(CommunicationSocket,
                        SampleConfiguration.Host,
                        SampleConfiguration.ConnectionPort);
                    await Activity.connect();

                    IAsyncSynAck DHsession = new DHkeyExchange(Activity);

                    await EstablishDHkeySession(DHsession);

                    IOCallbackAsync Session = new IOHandlerDHMessage(Activity, sessionKey);
                    serverMessage=await ContinueSession(Session);

                }
                catch (Exception exception)
                {
                    CommunicationSocket.Dispose();
                    switch (SocketError.GetStatus(exception.HResult))
                    {
                        case SocketErrorStatus.HostNotFound:

                            throw;
                        default:

                            throw;
                    }

                }
            }

        }
        private async Task EstablishDHkeySession(IAsyncSynAck DHsession)
        {
            PublicKey = await DHsession.GetPublicKey();
            await DHsession.StartDHSession();
            sessionKey.SessionKey=await DHsession.RetrieveDHSessionKey(PublicKey);
        }

        public async Task<String> ContinueSession(IOCallbackAsync Session)
        {
            await Session.SendDHEncryptedMessage(SampleConfiguration.Messages);
            return await Session.ReceiveDHEncryptedMessage(PublicKey);
           // await Session.SendDHEncryptedMessage("EndSession");
        }

        private String serverMessage;
        public String ServerMessage
        {
            get { return serverMessage; }
            set { serverMessage = value; }
        }


    }
}
