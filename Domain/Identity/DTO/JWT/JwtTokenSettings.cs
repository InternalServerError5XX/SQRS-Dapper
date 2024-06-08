using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.Identity.DTO.JWT
{
    public class JwtTokenSettings
    {
        public string JwtIssuer { get; set; } = string.Empty;
        public string JwtAudience { get; set; } = string.Empty;
        public string JwtKey { get; set; } = string.Empty;
        public int JwtExpires { get; set; }
    }
}
