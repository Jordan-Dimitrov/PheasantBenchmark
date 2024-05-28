using PheasantBench.Domain;
using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Application.Dtos
{
    public class CreateForumThreadDto
    {
        [Required]
        [MinLength(Constants.NameMinSize)]
        [MaxLength(Constants.NameSize)]
        public string Name { get; set; } = null!;
        [MinLength(Constants.NameMinSize)]
        [MaxLength(Constants.DescriptionSize)]
        public string Description { get; set; } = null!;
    }
}
