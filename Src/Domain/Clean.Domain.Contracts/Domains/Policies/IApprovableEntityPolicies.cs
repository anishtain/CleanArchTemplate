using Clean.Domain.Entities.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Domains.Policies
{
    public interface IApprovableEntityPolicies
    {
        bool IsApproved(ApprovableEntity entity);

        bool ChangeState(ApprovableEntity entity);
    }
}
