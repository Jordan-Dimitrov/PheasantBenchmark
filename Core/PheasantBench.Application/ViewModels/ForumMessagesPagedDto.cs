namespace PheasantBench.Application.ViewModels
{
    public class ForumMessagesPagedDto
    {
        public IEnumerable<ForumMessageDto> ForumMessages { get; set; }
        public int TotalPages { get; set; }
    }
}
