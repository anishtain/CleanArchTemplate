using Clean.Infrastructure.Datas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.Configurations
{
    internal class UserConfig : IEntityTypeConfiguration<Models.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(256)");

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnType("varchar(8)")
                .IsRequired();
        }
    }
}
