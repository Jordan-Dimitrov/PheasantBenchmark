namespace PheasantBench.Application.ViewModels
{
    public class BenchmarkDto
    {
        public string ProcessorName { get; set; } = null!;
        public string Architecture { get; set; } = null!;
        public string MachineName { get; set; } = null!;
        public string OsVersion { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public long Score { get; set; }
        public UserDto User { get; set; }
    }
}
