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
        [MaxLength(16)]
        public string Name { get; set; } = null!;
        [MaxLength(64)]
        public string Description { get; set; } = null!;
        public ICollection<ForumMessage> ForumMessages { get; set; }
    }
}
