using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PheasantBench.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Infrastructure.Configurations
{
    internal class UserUpvoteConfiguration : IEntityTypeConfiguration<UserUpvotes>
    {
        public void Configure(EntityTypeBuilder<UserUpvotes> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ForumMessageId });

            builder
                .Property<byte[]>("Version");

            builder.
                Property("Version")
                .IsRowVersion();

            builder.HasOne(u => u.User)
                .WithMany(u => u.UserUpvotes)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.ForumMessage)
                .WithMany(fm => fm.UserUpvotes)
                .HasForeignKey(u => u.ForumMessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
