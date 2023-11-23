using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using GrupoEstudos.Application.Interfaces;
using GrupoEstudos.Application.Mappings;
using GrupoEstudos.Application.Services;
using GrupoEstudos.Domain.Interfaces;
using GrupoEstudos.Infra.Data.Context;
using GrupoEstudos.Infra.Data.Repositories;

namespace GrupoEstudos.Infra.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IHostEnvironment hostEnvironment,
        IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //                        x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        //);

        //mysql
        string mySqlConnection = configuration.GetConnectionString("DefaultConnectionAuth");
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(mySqlConnection, 
            ServerVersion.AutoDetect(mySqlConnection))
        );
        
        //if(hostEnvironment.IsProduction())
        //{
        //    services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();
        //}

        //Registry Repositories        
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        //services.AddScoped<ISalaRepository, SalaRepository>();
        //services.AddScoped<ITarefaRepository, TarefaRepository>();
        //services.AddScoped<IChatRepository, ChatRepository>();
        //services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();

        //Registry Services        
        //services.AddScoped<ISendMailService, SendMailService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        //services.AddScoped<ISalaService, SalaService>();
        //services.AddScoped<ITarefaService, TarefaService>();
        //services.AddScoped<IChatService, ChatService>();
        //services.AddScoped<ICampeonatoService, CampeonatoService>();

        //AutoMapper
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        
        return services;
    }
}
