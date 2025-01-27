using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.Users.Extensions
{
    public static class UserModuleExtensions
    {
        public static void AddUserModule(this IServiceCollection services, IConfiguration configuration, List<Assembly> mediatorAssemblies)
        {

            var connectionString = configuration.GetConnectionString("UsersConnectionString");
            services.AddDbContext<UsersDBContext>(config => config.UseSqlServer(connectionString));
        }
    }
}
