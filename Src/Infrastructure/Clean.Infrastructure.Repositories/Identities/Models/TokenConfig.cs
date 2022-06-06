using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Repositories.Identities.Models
{
    internal class TokenConfig
    {
        public string? Issuer { get; set; }

        public string? Audience { get; set; }

        public string? Secret { get; set; }
    }
}
