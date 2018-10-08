using System;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ResuppliesPerMglt
{
    class ConsoleApp
    {
        enum ExitCodes
        {
            Success = 0,
            Failure
        }

        readonly CommandLineApplication app;
        readonly ResuppliesPerMgltService resuppliesPerMgltService;

        protected internal CommandOption DistanceInMglt { get; protected set; }

        protected internal CommandOption StarshipId { get; protected set; }

        public ConsoleApp(ResuppliesPerMgltService resuppliesPerMgltService, CommandLineApplication app)
        {
            this.resuppliesPerMgltService = resuppliesPerMgltService;
            this.app = app;
        }

        public void Run(string[] args)
        {
            InitializeApp();

            app.OnExecute(OnExecute);

            try
            {
                app.Execute(args);
            }
            catch (AggregateException e)
            {
                app.Error.WriteLine($"Error encountered: {e.GetBaseException().Message}");
                Environment.ExitCode = (int)ExitCodes.Failure;
            }
        }

        void InitializeApp()
        {
            app.Description = "Console SWApi.co client app";
            app.Conventions.UseDefaultConventions();

            DistanceInMglt = app.Option("-d|--distance", "The distance (in Megalights) the starship(s) are to travel. Mandatory argument.",
                CommandOptionType.SingleValue);

            StarshipId = app.Option("-i|--starship-id",
                "The starship ID the number of stops to re-supply needs to be calculated for. Optional argument.",
                CommandOptionType.SingleValue);

            app.ExtendedHelpText =
                "Simple console app that fetches all starships from https://swapi.co and lists how many re-supply stops each would need to travel a certain distance.";

            app.VersionOption("-v|--version",
                () => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
        }

        async Task<int> OnExecute()
        {
            if (!DistanceInMglt.HasValue())
            {
                app.ShowHint();
                return (int)ExitCodes.Success;
            }

            if (!long.TryParse(DistanceInMglt.Value(), out var distanceInMglt))
            {
                app.Error.WriteLine("Invalid numeric value entered.");
                return (int)ExitCodes.Failure;
            }

            if (StarshipId.HasValue())
            {
                var (starshipName, stopsToResupply, errorMessage) = await resuppliesPerMgltService.CalculateForStarshipAsync(StarshipId.Value(), distanceInMglt);

                app.Out.WriteLine($"{starshipName}: {stopsToResupply?.ToString() ?? errorMessage}");

                return (int)ExitCodes.Success;
            }

            foreach (var (starshipName, stopsToResupply, errorMessage) in await resuppliesPerMgltService.CalculateForAllStarshipsAsync(distanceInMglt))
            {
                app.Out.WriteLine($"{starshipName}: {stopsToResupply?.ToString() ?? errorMessage}");
            }

            return (int)ExitCodes.Success;
        }
    }
}