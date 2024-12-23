using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Options
{
    public class JwtSettings
    {
        public static readonly string SectionName = "JwtSettings";
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public double TokenExpiryTimeHour { get; set; }
    }
}
