using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Domain.Models
{
    public class ForumThread : Entity
    {
        public ForumThread()
        {
            Id = Guid.NewGuid();
            ForumMessages = new List<ForumMessage>();
        }
        [Required]
        [MaxLength(16)]
        public string Name { get; set; } = null!;
        [MaxLength(64)]
        public string Description { get; set; } = null!;
        public ICollection<ForumMessage> ForumMessages { get; set; }
    }
}
