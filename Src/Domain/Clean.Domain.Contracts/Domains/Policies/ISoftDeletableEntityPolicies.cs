using Clean.Domain.Entities.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Domains.Policies
{
    public interface ISoftDeletableEntityPolicies
    {
        bool IsDeleted(SoftDeletableEntity entity);

        void SoftDelete(SoftDeletableEntity entity);

        void Retrive(SoftDeletableEntity entity);
    }
}
