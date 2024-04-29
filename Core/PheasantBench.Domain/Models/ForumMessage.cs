using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Domain.Models
{
    public class ForumMessage : Entity
    {
        public ForumMessage()
        {
            Id = Guid.NewGuid();
            UserUpvotes = new List<UserUpvotes>();
            Replies = new List<ForumMessage>();
        }

        [Required]
        [MaxLength(1024)]
        public string MessageContent {  get; set; }
        [MaxLength(1024)]
        public string? FileName {  get; set; }
        [ForeignKey(nameof(Reply))]
        public Guid? ReplyId {  get; set; }
        public ForumMessage? Reply { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [ForeignKey(nameof(ForumThread))]
        public Guid ForumThreadId { get; set; }
        public ForumThread ForumThread { get; set; }
        public long UpvoteCount {  get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserUpvotes> UserUpvotes { get; set; }
        public ICollection<ForumMessage> Replies { get; set; }
    }
}
