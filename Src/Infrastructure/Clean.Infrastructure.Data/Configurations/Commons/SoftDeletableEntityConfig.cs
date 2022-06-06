using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Configurations.Commons
{
    internal abstract class SoftDeletableEntityConfig<T> : ApprovableEntityConfig<T> where T : Domain.Entities.Models.Commons.SoftDeletableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.DeleterId)
                .HasColumnType("varchar(64)");

            builder.HasIndex(x => x.IsDeleted)
                .IsClustered(false);
        }
    }
}
