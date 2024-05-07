using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateBenchmarkDto
    {
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
        public long Score { get; set; }
    }
}
