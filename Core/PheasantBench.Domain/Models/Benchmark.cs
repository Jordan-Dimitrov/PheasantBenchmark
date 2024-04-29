using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Domain.Models
{
    public class Benchmark : Entity
    {
        public Benchmark()
        {
            Id = Guid.NewGuid();
        }

        [MaxLength(255)]
        [Required]
        public string ProcessorName { get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string Architecture {  get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string MachineName {  get; set; } = null!;
        [MaxLength(255)]
        [Required]
        public string OsVersion {  get; set; } = null!;
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public long Score { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId {  get; set; }
        public User User { get; set; }
    }
}
