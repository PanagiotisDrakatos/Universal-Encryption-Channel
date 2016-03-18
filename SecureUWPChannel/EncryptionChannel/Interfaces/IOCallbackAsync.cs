using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureUWPChannel.Serialization;

namespace SecureUWPChannel.Interfaces
{
    public interface IOCallbackAsync
    {
        Task SendDHEncryptedMessage(String Message);
        Task<String> ReceiveDHEncryptedMessage();
    }
}
