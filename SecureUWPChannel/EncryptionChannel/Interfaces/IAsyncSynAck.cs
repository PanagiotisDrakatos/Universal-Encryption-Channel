using SecureUWPChannel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionChannel.Interfaces
{
    public interface IAsyncSynAck
    {
        Task<String> GetPublicKey();
        Task StartDHSession();
        Task<String> RetrieveDHSessionKey();

    }
}
