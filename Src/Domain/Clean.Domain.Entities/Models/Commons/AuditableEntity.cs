using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Entities.Models.Commons
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string? UpdatorId { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
