using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateForumThreadDto
    {
        [Required]
        [MaxLength(16)]
        public string Name { get; set; } = null!;
        [MaxLength(64)]
        public string Description { get; set; } = null!;
    }
}
