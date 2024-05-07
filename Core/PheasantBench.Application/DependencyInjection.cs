using Microsoft.Extensions.DependencyInjection;

namespace PheasantBench.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            return services;
        }
    }
}
