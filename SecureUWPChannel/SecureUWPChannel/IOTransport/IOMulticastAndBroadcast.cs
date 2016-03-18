using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using SecureUWPChannel.Serialization;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using System.Threading;
using SecureUWPChannel.Prooperties;

namespace SecureUWPChannel.IOTransport
{
    public class IOMulticastAndBroadcast
    {
        private string utf8String;
        private StreamSocket socket;
        public byte[] utf8Bytes;
        private HostParameters SocksParameters;
        //   private String protocol;


        public IOMulticastAndBroadcast(StreamSocket socket, String host, String port)
        {
            SocksParameters = new HostParameters();
            SocksParameters.Host = host;
            SocksParameters.Port = port;
            this.socket = socket;
        }

        public async Task connect()
        {


            try
            {
                HostName hostName;

                hostName = new HostName(SocksParameters.Host);
                socket.Control.NoDelay = false;
                var cts = new CancellationTokenSource();
                cts.CancelAfter(SampleConfiguration.timeout);

                // Connect to the server
                var connectAsync = socket.ConnectAsync(hostName, SocksParameters.Port);
                var connectTask = connectAsync.AsTask(cts.Token);
                await connectTask;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                await new MessageDialog("Make sure your Server is open and make sure you follow Instructions To connect localhost").ShowAsync();
                Application.Current.Exit();
            }

        }


        public async Task send(String message)
        {

            DataWriter writer;
            // Create the data writer object backed by the in-memory stream. 
            using (writer = new DataWriter(socket.OutputStream))
            {
                try
                {
                    Encoding utf8 = Encoding.UTF8;
                    Encoding unicode = Encoding.Unicode;
                    // Set the Unicode character encoding for the output stream
                    writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    // Specify the byte order of a stream.
                    //  writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

                    // Gets the size of UTF-8 string.

                    Int32 len = (int)writer.MeasureString(message);
                    byte[] toSendBytes = unicode.GetBytes(message);
                    utf8Bytes = Encoding.Convert(unicode, utf8, toSendBytes);
                    byte[] toSendLenBytes = System.BitConverter.GetBytes(len);
                    writer.WriteBytes(toSendLenBytes);
                    writer.WriteBytes(utf8Bytes);
                    // Write a string value to the output stream.




                    await writer.StoreAsync();
                    await writer.FlushAsync();

                    writer.DetachStream();
                    writer.Dispose();
                }
                catch (Exception exception)
                {
                    switch (SocketError.GetStatus(exception.HResult))
                    {
                        case SocketErrorStatus.HostNotFound:
                            // Handle HostNotFound Error
                            throw;
                        default:
                            // If this is an unknown status it means that the error is fatal and retry will likely fail.
                            throw;
                    }
                }


            }
        }


        public async Task<String> read()
        {
            DataReader reader;
            StringBuilder strBuilder;



            using (reader = new DataReader(socket.InputStream))
            {

                try
                {
                    strBuilder = new StringBuilder();
                    Encoding utf8 = Encoding.UTF8;
                    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    reader.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

                    // Get the size of the buffer that has not been read.
                    //System.Diagnostics.Debug.WriteLine("panos");
                    await reader.LoadAsync(4);
                    byte[] rcvLenBytes = new byte[4];
                    reader.ReadBytes(rcvLenBytes);
                    int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                    byte[] rcvBytes = new byte[rcvLen];
                    uint size = (uint)(int)rcvBytes.Length;

                    await reader.LoadAsync(size);
                    reader.ReadBytes(rcvBytes);
                    utf8String = Encoding.UTF8.GetString(rcvBytes, 0, Convert.ToInt32(rcvLen));

                    //   await new MessageDialog(utf8String).ShowAsync();
                    reader.DetachStream();
                    reader.Dispose();
                    return utf8String;
                }
                catch (Exception e)
                {
                    //System.Diagnostics.Debug.WriteLine("Catch");
                    //  await new MessageDialog(utf8String+e.ToString()).ShowAsync();           
                    return (e.ToString());

                }
            }

        }


        public async void close()
        {
            await socket.CancelIOAsync();
        }

        private class HostParameters
        {
            private String host;
            private String port;

            public string Host
            {
                get
                {
                    return host;
                }
                set
                {
                    host = value;
                }
            }


            public string Port
            {
                get
                {
                    return port;
                }
                set
                {
                    port = value;
                }
            }

        }

    }
}
