using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Models
{
    public class Role : IdentityRole<string>
    {
        public string? PersianName { get; set; }
    }
}
