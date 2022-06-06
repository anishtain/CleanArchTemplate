using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Configurations.Commons
{
    internal abstract class AuditableEntityConfig<T> : BaseEntityConfig<T> where T : Domain.Entities.Models.Commons.AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UpdatorId)
                .HasColumnType("varchar(64)");

            builder.HasIndex(x => x.UpdatorId)
                .IsClustered(false);
        }
    }
}
