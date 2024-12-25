using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Authentication.Login
{
    public class LoginQueryDto
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}