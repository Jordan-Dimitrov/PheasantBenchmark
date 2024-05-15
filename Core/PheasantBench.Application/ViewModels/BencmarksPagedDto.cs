namespace PheasantBench.Application.ViewModels
{
    public class BencmarksPagedDto
    {
        public IEnumerable<BenchmarkDto> BenchmarkDtos { get; set; }
        public int TotalPages { get; set; }
    }
}
