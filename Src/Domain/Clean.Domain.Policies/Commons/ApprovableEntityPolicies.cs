using Clean.Domain.Entities.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Policies.Commons
{
    internal class ApprovableEntityPolicies : Contracts.Domains.Policies.IApprovableEntityPolicies
    {
        public bool IsApproved(ApprovableEntity entity)
            => entity.IsActive;

        public bool ChangeState(ApprovableEntity entity)
            => entity.IsActive = !entity.IsActive;
    }
}
