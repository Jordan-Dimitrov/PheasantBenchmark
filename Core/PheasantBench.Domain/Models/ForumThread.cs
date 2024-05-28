using System.ComponentModel.DataAnnotations;

namespace PheasantBench.Domain.Models
{
    public class ForumThread
    {
        public ForumThread()
        {
            Id = Guid.NewGuid();
            ForumMessages = new List<ForumMessage>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(Constants.NameSize)]
        public string Name { get; set; } = null!;
        [MaxLength(Constants.DescriptionSize)]
        public string Description { get; set; } = null!;
        public ICollection<ForumMessage> ForumMessages { get; set; }
    }
}
