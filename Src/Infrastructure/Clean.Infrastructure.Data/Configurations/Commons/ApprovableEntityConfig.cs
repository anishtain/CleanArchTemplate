using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Configurations.Commons
{
    internal abstract class ApprovableEntityConfig<T> : AuditableEntityConfig<T> where T : Domain.Entities.Models.Commons.ApprovableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasIndex(x => x.IsActive)
                .IsClustered(false);
        }
    }
}
