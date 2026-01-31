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

            builder.Property(n => n.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(n => n.Title)
                   .HasColumnName("title")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(n => n.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

            builder.Property(n => n.CompletedAt)
                   .HasColumnName("completed_at");

            builder.Property(n => n.DueAt)
                   .HasColumnName("due_at")
                   .IsRequired();

            builder.Property(n => n.IsCompleted)
                   .HasColumnName("is_completed")
                   .IsRequired();

            builder.Property(n => n.AttachmentPath)
                   .HasColumnName("attachment_path");

            builder.HasIndex(x => x.IsCompleted);
            builder.HasIndex(x => x.DueAt);
        }
    }
}
