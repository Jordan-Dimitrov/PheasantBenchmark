namespace PheasantBench.Application.ViewModels
{
    public class ForumMessageDto
    {
        public string MessageContent { get; set; }
        public string? FileName { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ForumThreadId { get; set; }
        public long UpvoteCount { get; set; }
    }
}
