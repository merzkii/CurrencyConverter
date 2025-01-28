using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core;


public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(3); 
        builder.Property(e => e.Rate)
            .IsRequired()
            .HasColumnType("decimal(18,4)");
        builder.Property(e => e.Date)
            .IsRequired();

        builder.ToTable("ExchangeRates");
    }
}
