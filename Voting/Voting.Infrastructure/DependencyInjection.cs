using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Voting.Application.Interfaces;
using Voting.Infrastructure.Persistence;

namespace Voting.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddDatabase(configuration);
        }
        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VotingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Voting"));
            });

            services.AddScoped<IVotingDbContext>(provider => provider.GetService<VotingDbContext>());
        }
    }
}
