using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Abstractions
{
    public abstract class Deletable
    {
        public bool IsDeleted { get; set; } = false;
    }
}
