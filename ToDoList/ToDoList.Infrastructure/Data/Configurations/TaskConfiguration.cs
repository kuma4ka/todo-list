using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Infrastructure.Data.Configurations;

public class TaskConfiguration: IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.HasIndex(t => t.TaskTitle)
            .IsUnique();
        
        builder.HasOne(t => t.Creator)
            .WithMany(u => u.Tasks)
            .IsRequired();
    }
}