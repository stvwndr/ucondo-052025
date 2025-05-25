using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UCondoApp.Domain.Entitites;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Infra.Data.Configurations;

public class AccountsChartEntityConfiguration : IEntityTypeConfiguration<AccountsChart>
{
    public void Configure(EntityTypeBuilder<AccountsChart> builder)
    {
        builder.ToTable("accountschart");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("accountschartid").IsRequired();
        builder.Property(x => x.Code).HasMaxLength(30).IsRequired();
        builder.Property(x => x.FormattedCode).HasMaxLength(50).IsRequired();
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
