using PheasantBench.Domain;
using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateBenchmarkDto
    {
        [MinLength(Constants.NameSize)]
        [MaxLength(Constants.HardwareInfoSize)]
        [Required]
        public string ProcessorName { get; set; } = null!;
        [MinLength(Constants.NameSize)]
        [MaxLength(Constants.HardwareInfoSize)]
        [Required]
        public string Architecture { get; set; } = null!;
        [MinLength(Constants.NameSize)]
        [MaxLength(Constants.HardwareInfoSize)]
        [Required]
        public string MachineName { get; set; } = null!;
        [MinLength(Constants.NameSize)]
        [MaxLength(Constants.HardwareInfoSize)]
        [Required]
        public string OsVersion { get; set; } = null!;
        [Required]
        public long Score { get; set; }
    }
}
