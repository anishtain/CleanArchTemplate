using Clean.Domain.Entities.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Policies.Commons
{
    internal class SoftDeletableEntityPolicies : Contracts.Domains.Policies.ISoftDeletableEntityPolicies
    {
        public bool IsDeleted(SoftDeletableEntity entity)
            => entity.IsDeleted;

        public void SoftDelete(SoftDeletableEntity entity)
            => entity.IsDeleted = true;

        public void Retrive(SoftDeletableEntity entity)
            => entity.IsDeleted = false;
    }
}
