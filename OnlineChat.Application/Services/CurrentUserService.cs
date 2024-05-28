using Microsoft.AspNetCore.Http;
using OnlineChat.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor _contextAccessor)
        {
            var httpContext = _contextAccessor.HttpContext;
            var userClaims = httpContext?.User.Claims;
            var idClaim = userClaims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim != null && Guid.TryParse(idClaim.Value, out Guid value))
            {
                Console.WriteLine($"UserId has: {value}");
                UserId = value;
            }
        }
/*        public Guid UserId { get; private set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserId = userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
        }*/
    }
}
