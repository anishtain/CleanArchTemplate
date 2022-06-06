using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Entities.Models.Commons
{
    public abstract class ApprovableEntity : AuditableEntity
    {
        public bool IsActive { get; set; }
    }
}
