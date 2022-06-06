using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Configurations.Commons
{
    internal abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : Domain.Entities.Models.Commons.BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatorId)
                .IsRequired()
                .HasColumnType("varchar(64)");

            builder.Property(x => x.CreationDate)
                .IsRequired();

            builder.HasIndex(x => x.CreatorId)
                .IsClustered(false);
        }
    }
}
