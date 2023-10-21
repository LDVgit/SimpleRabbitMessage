namespace Consumer.EntityConfigurations
{
    using Consumer.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <inheritdoc />
    internal class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("site");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Url)
                .HasColumnName("url")
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(x => x.Response)
                .HasColumnName("response");
        }
    }
}