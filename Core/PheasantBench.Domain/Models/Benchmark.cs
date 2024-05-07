﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PheasantBench.Domain.Models
{
    public class Benchmark
    {
        public Benchmark()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string ProcessorName { get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string Architecture { get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string MachineName { get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string OsVersion { get; set; } = null!;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public long Score { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
