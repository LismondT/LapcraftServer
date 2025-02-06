using Microsoft.EntityFrameworkCore;

using LapcraftServer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LapcraftServer.Persistens.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
