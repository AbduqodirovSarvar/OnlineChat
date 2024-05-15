using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Abstractions
{
    public interface IHashService
    {
        string GetHash(string password);
        bool VerifyHash(string password, string paswordHash);
    }
}
