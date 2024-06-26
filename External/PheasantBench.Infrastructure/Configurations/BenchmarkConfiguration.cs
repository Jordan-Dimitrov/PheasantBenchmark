﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PheasantBench.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Infrastructure.Configurations
{
    internal class BenchmarkConfiguration : IEntityTypeConfiguration<Benchmark>
    {
        public void Configure(EntityTypeBuilder<Benchmark> builder)
        {
            builder
                .Property<byte[]>("Version");

            builder.
                Property("Version")
                .IsRowVersion();
        }
    }
}
