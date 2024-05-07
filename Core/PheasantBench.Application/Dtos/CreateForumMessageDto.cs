using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateForumMessageDto
    {
        [Required]
        [MaxLength(1024)]
        public string MessageContent { get; set; }
        [MaxLength(1024)]
        public string? FileName { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public Guid ForumThreadId { get; set; }
    }
}
