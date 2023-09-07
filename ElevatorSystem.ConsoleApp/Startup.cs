
using ElevatorSystem.Application.Interfaces;
using ElevatorEngine.Domain.Interfaces;
using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Application.Mapper;
using ElevatorSystem.Application.Services;
using ElevatorSystem.ConsoleApp.Utilities;
using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Infrastructure.Data;
using ElevatorSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ElevatorSystem.ConsoleApp {
    public class Startup {
        public static void Main(string[] args) {
            using IHost host = CreateHostBuilder(args).Build();
            var services = host.Services;
            var elevatorService = services.GetRequiredService<IElevatorService>();
            var floorService = services.GetRequiredService<IFloorService>();
            var simulationManager = services.GetRequiredService<SimulationManager>();
             simulationManager.StartAsync();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) => {
               config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
           })
           .ConfigureServices((hostContext, services) => {
              
               services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(hostContext.Configuration.GetConnectionString("ElevatorConnection")));
              
               services.AddTransient<IFloorRepository, FloorRepository>();
               services.AddTransient<IElevatorRepository, ElevatorRepository>();
               services.AddScoped<IUnitOfWork, UnitOfWork>();
               services.AddAutoMapper(typeof(Startup));
               services.AddAutoMapper(typeof(MappingProfile).Assembly);
               services.AddScoped<IFloorService, FloorService>();
               services.AddScoped<IElevatorService, ElevatorService>();
               services.AddTransient<IElevatorOrchestratorService, ElevatorOrchestratorService>();
               services.AddSingleton<SimulationManager>();
           });
    }
 }
