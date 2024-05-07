using Microsoft.AspNetCore.Identity;

namespace PheasantBench.Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Benchmarks = new List<Benchmark>();
            ForumMessages = new List<ForumMessage>();
            UserUpvotes = new List<UserUpvotes>();
        }
        public ICollection<Benchmark> Benchmarks { get; set; }
        public ICollection<ForumMessage> ForumMessages { get; set; }
        public ICollection<UserUpvotes> UserUpvotes { get; set; }
    }
}
