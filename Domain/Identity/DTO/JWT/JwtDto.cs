using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.Identity.DTO.JWT
{
    public class JwtDto
    {
        public JwtSecurityToken? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
