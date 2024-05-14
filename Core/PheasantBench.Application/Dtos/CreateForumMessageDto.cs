using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateForumMessageDto
    {
        [Required]
        [MaxLength(1024)]
        public string MessageContent { get; set; }
        [Required]
        public Guid ForumThreadId { get; set; }
        public IFormFile? File { get; set; }
    }
}
