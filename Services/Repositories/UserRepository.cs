using Microsoft.AspNetCore.Http;
using MoveNowB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoveNowB.Services.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
