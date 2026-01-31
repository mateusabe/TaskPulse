using TaskPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskPulse.Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("task");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.DueAt)
                .IsRequired();

            builder.Property(x => x.IsCompleted)
                .IsRequired();

            builder.Property(x => x.AttachmentPath)
                .IsRequired();

            builder.HasIndex(x => x.IsCompleted);
            builder.HasIndex(x => x.DueAt);
        }
    }
}
