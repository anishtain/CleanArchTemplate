using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Entities.Models.Commons
{
    public abstract class SoftDeletableEntity : ApprovableEntity
    {
        public bool IsDeleted { get; set; }

        public string? DeleterId { get; set; }

        public DateTime? DeletionDate { get; set; }
    }
}
