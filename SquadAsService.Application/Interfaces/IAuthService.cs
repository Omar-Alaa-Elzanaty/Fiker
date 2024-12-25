using Fiker.Domain.Domains.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user, string role, bool rembemerMe = false);
    }
}
