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
    internal class RoleConfig : IEntityTypeConfiguration<Models.Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.PersianName)
                .HasColumnType("nvarchar(128)");
        }
    }
}
