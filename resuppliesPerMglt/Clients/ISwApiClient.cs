using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResuppliesPerMglt.Models;

namespace ResuppliesPerMglt.Clients
{
    /// <summary>
    /// Defines https://swapi.co API client 
    /// </summary>
    public interface ISwApiClient
    {
        /// <summary>
        /// Gets a single starship from the API
        /// </summary>
        /// <param name="id">The ID of the starship, as used in the API</param>
        /// <returns></returns>
        Task<StarshipEntity> GetStarshipAsync(string id);

        /// <summary>
        /// Gets starships in paginated batches
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PaginatedResultsEntity<StarshipEntity>> GetStarshipsPaginatedAsync(string page = "1");

        /// <summary>
        /// Composes and returns a list of all starships
        /// </summary>
        /// <returns></returns>
        Task<IList<StarshipEntity>> GetAllStarshipsAsync();

        /// <summary>
        /// Composes and returns a list of all starships, while firing a callback after receiving each batch
        /// </summary>
        /// <param name="pageCallback"></param>
        /// <returns></returns>
        Task GetAllStarshipsAsync(Action<IList<StarshipEntity>> pageCallback);
    }
}