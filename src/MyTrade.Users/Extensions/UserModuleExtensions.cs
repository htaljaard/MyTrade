using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTrade.Users.Domain;
using MyTrade.Users.Infrastructure.Data;
using Serilog;
using System.Reflection;

namespace MyTrade.Users.Extensions
{
    public static class UserModuleExtensions
    {
        public static void AddUserModule(this IServiceCollection services, IConfiguration configuration, List<Assembly> mediatorAssemblies, ILogger logger)
        {
            logger.Information("Starting Module: {0}", "User Module");
            var connectionString = configuration.GetConnectionString("UsersConnectionString");
            services.AddDbContext<UsersDBContext>(config => config.UseSqlServer(connectionString));

            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDBContext>();

            mediatorAssemblies.Add(typeof(UserModuleExtensions).Assembly);
        }
    }
}
