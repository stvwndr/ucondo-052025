using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCondoApp.Domain.Entitites;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;
using static Dapper.SqlMapper;

namespace UCondoApp.Infra.Data.Configurations;

public class AccountsChartEntityConfiguration : IEntityTypeConfiguration<AccountsChart>
{
    public void Configure(EntityTypeBuilder<AccountsChart> builder)
    {
        builder.ToTable("accountschart");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("accountschartid").IsRequired();
        builder.Property(x => x.Code).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.AccountType)
            .IsRequired()
            .HasColumnType("varchar(30)")
            .HasConversion(new EnumToStringConverter<AccountsChartType>());
        builder.HasOne(d => d.ParentAccount)
            .WithMany()
            .HasForeignKey(d => d.ParentAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
