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
        private readonly IUserStore<User> _UserStore;
        public Seed(ApplicationDbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IUserStore<User> userStore)
        {
            _Context = context;
            _UserManager = userManager;
            _RoleManager = roleManager;
            _UserStore = userStore;
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

                User user = new User();
                User admin = new User();

                await _UserStore.SetUserNameAsync(admin, "admin@gmail.com", default);
                await ((IUserEmailStore<User>)_UserStore).SetEmailAsync(admin, "admin@gmail.com", default);
                await ((IUserEmailStore<User>)_UserStore).SetEmailConfirmedAsync(admin, true, default);
                await _UserManager.CreateAsync(admin, "Pr0toty1pe@1");

                await _UserManager.AddToRoleAsync(admin, Role.Admin.ToString());

                await _UserStore.SetUserNameAsync(user, "tomaaa@gmail.com", default);
                await ((IUserEmailStore<User>)_UserStore).SetEmailAsync(user, "tomaaa@gmail.com", default);
                await ((IUserEmailStore<User>)_UserStore).SetEmailConfirmedAsync(user, true, default);
                await _UserManager.CreateAsync(user, "Pr0toty1pe@1");

                await _UserManager.AddToRoleAsync(user, Role.User.ToString());

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
                        UserId = admin.Id,
                        DateCreated = DateTime.Now
                },
                    new ForumMessage()
                    {
                        MessageContent = "TOMAAAA",
                        ForumThreadId = threads[1].Id,
                        UserId = user.Id,
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
                        UserId = admin.Id,
                },
                    new Benchmark()
                    {
                        ProcessorName = "i7 2600",
                        Architecture = "x86",
                        MachineName = "MONSTAR2",
                        OsVersion = "Windows 10",
                        DateCreated = DateTime.Now,
                        Score = 200,
                        UserId = user.Id,
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