using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prompt.Domain.Entities;

namespace Prompt.Infrastructure.Persistence.Configurations;

public class PromptJobConfiguration : IEntityTypeConfiguration<PromptJob>
{
    public void Configure(EntityTypeBuilder<PromptJob> builder)
    {
        builder.ToTable("PromptJobs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Prompt)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Response);

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(4000);

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.StartedAtUtc);

        builder.Property(x => x.CompletedAtUtc);
    }
}