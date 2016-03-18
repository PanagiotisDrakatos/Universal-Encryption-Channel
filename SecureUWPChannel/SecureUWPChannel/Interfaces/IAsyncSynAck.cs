using SecureUWPChannel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUWPChannel.Interfaces
{
    public interface IAsyncSynAck
    {
        Task<String> GetPublicKey();
        Task StartDHSession();
        Task<String> RetrieveDHSessionKey(String PublickKey);

    }
}
