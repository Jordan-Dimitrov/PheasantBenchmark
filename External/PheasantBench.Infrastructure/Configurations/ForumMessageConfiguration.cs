using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure.Configurations
{
    internal class ForumMessageConfiguration : IEntityTypeConfiguration<ForumMessage>
    {
        public void Configure(EntityTypeBuilder<ForumMessage> builder)
        {
            builder
                .Property<byte[]>("Version");

            builder.
                Property("Version")
                .IsRowVersion();
        }
    }
}
