using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IEncryptionService
    {
        (string encryptedMessage, byte[] iv) Encrypt(string message);
        string Decrypt(string encryptedMessage, byte[] iv);
    }
}
