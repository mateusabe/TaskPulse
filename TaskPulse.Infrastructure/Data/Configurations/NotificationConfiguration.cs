using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notification");

            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.TaskId);

            builder.Property(n => n.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(n => n.Message)
                   .HasColumnName("message")
                   .IsRequired();

            builder.Property(n => n.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

            builder.Property(n => n.ReadAt)
                   .HasColumnName("read_at");

            builder.Property(n => n.IsRead)
                   .HasColumnName("is_read")
                   .IsRequired();

            builder.Property(n => n.TaskId)
                   .HasColumnName("task_id")
                   .IsRequired();

            builder
                .HasOne(n => n.TaskInfo)
                .WithMany(i => i.notifications)
                .HasForeignKey(i => new { i.TaskId })
                .HasPrincipalKey(n => new { n.Id })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.IsRead);
        }
    }
}
