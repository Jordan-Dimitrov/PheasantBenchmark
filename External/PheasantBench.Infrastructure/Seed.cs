using Microsoft.AspNetCore.Identity;
using PheasantBench.Domain.Enums;
using PheasantBench.Domain.Models;

namespace PheasantBench.Infrastructure
{
    public class Seed
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public Seed(ApplicationDbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _Context = context;
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        public async Task SeedContext()
        {
            var hasher = new PasswordHasher<User>();

            if (!_Context.Users.Any())
            {
                if (!await _RoleManager.RoleExistsAsync(Role.User.ToString()))
                {
                    await _RoleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
                }

                if (!await _RoleManager.RoleExistsAsync(Role.Admin.ToString()))
                {
                    await _RoleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
                }

                List<User> users = new List<User>()
                {
                    new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                    new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "tomaaa",
                        NormalizedUserName = "TOMAAA",
                        Email = "tomaaa@gmail.com",
                        NormalizedEmail = "TOMAAA@GMAIL.COM",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    }
                };

                _Context.AddRange(users);
                _Context.SaveChanges();

                users[0].PasswordHash = hasher.HashPassword(users[0], "Pr0t1type");
                await _UserManager.AddToRoleAsync(users[0], Role.Admin.ToString());

                users[1].PasswordHash = hasher.HashPassword(users[1], "Pr0t1type");
                await _UserManager.AddToRoleAsync(users[1], Role.User.ToString());

                _Context.SaveChanges();

                List<ForumThread> threads = new List<ForumThread>()
                {
                    new ForumThread()
                    {
                        Name = "Old CPUs",
                        Description = "A thread for old cpu benchmark results"
                    },
                    new ForumThread()
                    {
                        Name = "New CPUs",
                        Description = "A thread for new cpu benchmark results"
                    }
                };

                _Context.ForumThreads.AddRange(threads);
                await _Context.SaveChangesAsync();

                List<ForumMessage> messages = new List<ForumMessage>()
                {
                    new ForumMessage()
                    {
                        MessageContent = "OC Monster",
                        ForumThreadId = threads[0].Id,
                        UserId = users[0].Id,
                        DateCreated = DateTime.Now
                },
                    new ForumMessage()
                    {
                        MessageContent = "TOMAAAA",
                        ForumThreadId = threads[1].Id,
                        UserId = users[1].Id,
                        DateCreated = DateTime.Now
                    }
                };

                _Context.ForumMessages.AddRange(messages);
                await _Context.SaveChangesAsync();

                List<Benchmark> benchmarks = new List<Benchmark>()
                {
                    new Benchmark()
                    {
                        ProcessorName = "Ryzen 5 5600",
                        Architecture = "x86",
                        MachineName = "MONSTAR",
                        OsVersion = "Windows 11",
                        DateCreated = DateTime.Now,
                        Score = 100,
                        UserId = users[1].Id,
                },
                    new Benchmark()
                    {
                        ProcessorName = "i7 2600",
                        Architecture = "x86",
                        MachineName = "MONSTAR2",
                        OsVersion = "Windows 10",
                        DateCreated = DateTime.Now,
                        Score = 200,
                        UserId = users[1].Id,
                    }
                };

                _Context.Benchmarks.AddRange(benchmarks);
                await _Context.SaveChangesAsync();

                List<UserUpvotes> userUpvotes = new List<UserUpvotes>()
                {
                    new UserUpvotes()
                    {
                        ForumMessageId = _Context.ForumMessages.First().Id,
                        UserId = _Context.Users.First().Id,
                        Rating = 1
                    }
                };

                _Context.UsersUpvotes.AddRange(userUpvotes);
                await _Context.SaveChangesAsync();
            }
        }
    }
}
