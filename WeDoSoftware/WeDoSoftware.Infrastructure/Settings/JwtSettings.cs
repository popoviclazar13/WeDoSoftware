using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDoSoftware.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public int ExpiresInMinutes { get; set; }
    }
}
