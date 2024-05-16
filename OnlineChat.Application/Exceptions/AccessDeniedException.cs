using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() 
            : base("Access Denied Exception")
        { }
    }
}
