using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.HasMany(u => u.Tasks)
            .WithOne(t => t.Creator)
            .OnDelete(DeleteBehavior.Cascade);
    }
}