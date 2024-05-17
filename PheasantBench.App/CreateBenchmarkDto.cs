using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.App
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
