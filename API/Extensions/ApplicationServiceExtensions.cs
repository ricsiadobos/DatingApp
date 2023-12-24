using API.Data;
using API.Interfaces;
using API.Servives;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        //Token kezelésre létrehozott service 
        services.AddScoped<ITokenService, TokenSevice>();

        //itt adjuk �t a db forrást a rendszerben
        services.AddDbContext<DataContext>(
            options => options.UseSqlServer(
                config.GetConnectionString(
                    "DefaultConnection")));
        return services;
    }
}