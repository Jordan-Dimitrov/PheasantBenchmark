namespace PheasantBench.Application.ViewModels
{
    public class ForumThreadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
