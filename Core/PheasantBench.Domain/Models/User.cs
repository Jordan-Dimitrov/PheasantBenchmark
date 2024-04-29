using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheasantBench.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
            Benchmarks = new List<Benchmark>();
            ForumMessages = new List<ForumMessage>(); 
            UserUpvotes = new List<UserUpvotes>();
        }
        public ICollection<Benchmark> Benchmarks { get; set; }
        public ICollection<ForumMessage> ForumMessages { get; set; }
        public ICollection<UserUpvotes> UserUpvotes { get; set; }
    }
}
