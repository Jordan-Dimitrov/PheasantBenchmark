using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PheasantBench.Application.Abstractions;
using PheasantBench.Domain.Abstractions;
using PheasantBench.Infrastructure.Repositories;
using PheasantBench.Infrastructure.Services;
using System.Reflection;

namespace PheasantBench.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string assemblyName = currentAssembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(assemblyName));
            });

            services.AddScoped<IBenchmarkRepository, BenchmarkRepository>();
            services.AddScoped<IForumMessageRepository, ForumMessageRepository>();
            services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserUpvoteService, UserUpvoteService>();
            services.AddScoped<IUserUpvoteRepository, UserUpvoteRepository>();

            return services;
        }
    }
}
