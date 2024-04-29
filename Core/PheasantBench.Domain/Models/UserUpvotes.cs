using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
