using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Models;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.ClientName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(o => o.PersonalNumber)
            .IsRequired()
            .HasMaxLength(11);
        builder.Property(o => o.FromCurrency)
            .IsRequired()
            .HasMaxLength(3);
        builder.Property(o => o.ToCurrency)
            .IsRequired()
            .HasMaxLength(3);
        builder.Property(o => o.Rate)
            .IsRequired()
            .HasColumnType("decimal(18,4)");
        builder.Property(o => o.Date)
            .IsRequired();
        builder.Property(o => o.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.ToTable("Operations"); 
    }
}
