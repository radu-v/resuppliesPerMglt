using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResuppliesPerMglt.Clients;
using ResuppliesPerMglt.Helpers;
using ResuppliesPerMglt.Models;

namespace ResuppliesPerMglt
{
    /// <summary>
    /// Service for calculating how many times a starship will need to stop to re-supply, in order to travel a certain distance
    /// </summary>
    public class ResuppliesPerMgltService
    {
        readonly ISwApiClient swApiClient;
        readonly IDurationParser durationParser;

        public ResuppliesPerMgltService(ISwApiClient swApiClient, IDurationParser durationParser)
        {
            this.swApiClient = swApiClient;
            this.durationParser = durationParser;
        }

        /// <summary>
        /// Fetches the details of all starships and calculates the number of re-supplies each needs to cover a distance
        /// </summary>
        /// <param name="distanceInMglt"></param>
        /// <returns>An enumerable of tuples of the starship name, number of re-supplies and an error message, if any</returns>
        public async Task<IEnumerable<(string StarshipName, long? StopsToResupply, string ErrorMessage)>> CalculateForAllStarshipsAsync(long distanceInMglt)
        {
            var starships = await swApiClient.GetAllStarshipsAsync();

            return ProcessStarships(starships, distanceInMglt);
        }

        /// <summary>
        /// Fetches the details of a starship and calculates the number of re-supplies it needs to cover a distance
        /// </summary>
        /// <param name="starshipId"></param>
        /// <param name="distanceInMglt"></param>
        /// <returns>A tuple of the starship name, number of re-supplies and an error message, if any</returns>
        public async Task<(string StarshipName, long? StopsToResupply, string errorMessage)> CalculateForStarshipAsync(string starshipId, long distanceInMglt)
        {
            var starship = await swApiClient.GetStarshipAsync(starshipId);

            return ProcessStarship(starship, distanceInMglt);
        }

        /// <summary>
        /// Calculates the number of re-supplies a ship with a certain speed and consumables storage needs to travel a certain distance
        /// </summary>
        /// <param name="distanceInMglt"></param>
        /// <param name="speedInMgltPerHour"></param>
        /// <param name="consumablesLifetimeHours">The amount of time a starship can provide consumables, until it needs to re-supply</param>
        /// <returns></returns>
        public static (long? stopsCount, string errorMessage) CalculateResupplyStops(long distanceInMglt, long? speedInMgltPerHour, decimal? consumablesLifetimeHours)
        {
            if (!speedInMgltPerHour.HasValue) return (default, "Unknown Megalights per Hour");
            if (consumablesLifetimeHours == null) return (default, "Unknown max period consumables are available");

            var distanceUntilConsumablesRunOut = speedInMgltPerHour * consumablesLifetimeHours;
            var stopsToResupply = distanceInMglt / distanceUntilConsumablesRunOut;
            var stopsToResupplyRounded = (long)stopsToResupply.Value;

            return (stopsToResupplyRounded, default);
        }

        (string StarshipName, long? StopsToResupply, string ErrorMessage) ProcessStarship(StarshipEntity starship, long distanceInMglt)
        {
            var megalightsPerHour = TypeParseHelper.LongParseOrNull(starship.MGLT);
            var (value, units) = durationParser.Parse(starship.Consumables);
            var consumableLifetimeInHours = durationParser.ConvertToHours(value, units);
            var (stopsCount, errorMessage) = CalculateResupplyStops(distanceInMglt, megalightsPerHour, consumableLifetimeInHours);

            return (starship.Name, stopsCount, errorMessage);
        }

        IEnumerable<(string StarshipName, long? StopsToResupply, string ErrorMessage)> ProcessStarships(IList<StarshipEntity> starships, long distanceInMglt) 
            => starships.Select(starship => ProcessStarship(starship, distanceInMglt));
    }
}