using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PheasantBench.Domain.Models
{
    public class UserUpvotes
    {
        [Required]
        [ForeignKey(nameof(ForumMessage))]
        public Guid ForumMessageId { get; set; }
        public ForumMessage ForumMessage { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; } = null!;
        public byte Rating { get; set; }
    }
}
