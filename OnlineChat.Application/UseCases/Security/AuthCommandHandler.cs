using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Exceptions;
using OnlineChat.Application.Models;
using OnlineChat.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application.UseCases.Security
{
    public class AuthCommandHandler(
        IAppDbContext context,
        ITokenService tokenService,
        IHashService hashService,
        ILogger<AuthCommandHandler> logger,
        IMapper mapper) : IRequestHandler<AuthCommand, LoginViewModel>
    {
        private readonly IAppDbContext _context = context;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHashService _hashService = hashService;
        private readonly ILogger<AuthCommandHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<LoginViewModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                     ?? throw new NotFoundException("User not found.");

            if (!_hashService.VerifyHash(request.Password, user.PasswordHash))
            {
                throw new LoginException();
            }

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            _logger.LogInformation("Gave access token for identifier Id: {Identifier}", user.Id);

            return new LoginViewModel(_mapper.Map<UserViewModel>(user), _tokenService.GetAccessToken([.. claims]));
        }
    }
}
