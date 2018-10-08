using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using ResuppliesPerMglt.Clients;
using ResuppliesPerMglt.Helpers;

namespace ResuppliesPerMglt
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            serviceProvider
                .GetService<ConsoleApp>()
                .Run(args);
        }

        static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IDurationParser, DurationParser>()
                .AddSingleton<ISwApiClient, SwApiClient>()
                .AddSingleton<ResuppliesPerMgltService>()
                .AddSingleton(new CommandLineApplication(false))
                .AddSingleton<ConsoleApp>()
                .BuildServiceProvider();
        }
    }
}
