using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PheasantBench.Domain.Models
{
    public class ForumMessage
    {
        public ForumMessage()
        {
            Id = Guid.NewGuid();
            UserUpvotes = new List<UserUpvotes>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(Constants.MessageSize)]
        public string MessageContent { get; set; }
        [MaxLength(Constants.MessageSize)]
        public string? FileName { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [ForeignKey(nameof(ForumThread))]
        public Guid ForumThreadId { get; set; }
        public ForumThread ForumThread { get; set; }
        public long UpvoteCount { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserUpvotes> UserUpvotes { get; set; }
    }
}
