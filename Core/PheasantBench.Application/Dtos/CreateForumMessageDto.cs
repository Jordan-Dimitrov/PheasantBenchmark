using Microsoft.AspNetCore.Http;
using PheasantBench.Domain;
using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateForumMessageDto
    {
        [Required]
        [MinLength(Constants.NameSize)]
        [MaxLength(Constants.MessageSize)]
        public string MessageContent { get; set; }
        [Required]
        public Guid ForumThreadId { get; set; }
        public IFormFile? File { get; set; }
    }
}
