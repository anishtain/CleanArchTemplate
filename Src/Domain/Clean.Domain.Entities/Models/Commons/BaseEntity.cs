using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Entities.Models.Commons
{
    public abstract class BaseEntity
    {
        public string? Id { get; set; }

        public string? CreatorId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
