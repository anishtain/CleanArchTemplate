using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Models
{
    public class User : IdentityUser<string>
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
