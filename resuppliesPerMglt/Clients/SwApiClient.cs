using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResuppliesPerMglt.Helpers;
using ResuppliesPerMglt.Models;

namespace ResuppliesPerMglt.Clients
{
    class SwApiClient : ISwApiClient
    {
        const string SwApiUrl = "https://swapi.co/api/";

        protected internal const string UserAgent = "SWApi Starship Resupplies per MGLT calculator";
        protected internal const string StarshipsEndpoint = "starships";

        static readonly HttpClient HttpClient = new HttpClient();

        public async Task<StarshipEntity> GetStarshipAsync(string id) => await GetSingleAsync<StarshipEntity>(StarshipsEndpoint, id);

        public async Task<PaginatedResultsEntity<StarshipEntity>> GetStarshipsPaginatedAsync(string page = "1") => await GetAllPaginatedAsync<StarshipEntity>(StarshipsEndpoint, page);

        public async Task<IList<StarshipEntity>> GetAllStarshipsAsync()
        {
            var result = new List<StarshipEntity>();

            await GetAllStarshipsAsync(starships => result.AddRange(starships));

            return result;
        }

        public async Task GetAllStarshipsAsync(Action<IList<StarshipEntity>> pageCallback)
        {
            if (pageCallback == null) throw new ArgumentNullException(nameof(pageCallback));
            var pageNumber = "1";

            do
            {
                var resultPage = await GetAllPaginatedAsync<StarshipEntity>(StarshipsEndpoint, pageNumber);
                pageCallback(resultPage.Results);
                pageNumber = UrlUtils.GetPageNumber(resultPage.Next);
            } while (!string.IsNullOrWhiteSpace(pageNumber));
        }

        static async Task<T> GetSingleAsync<T>(string endpoint, string id) where T : IBaseEntity
        {
            var json = await ApiRequestAsync($"{SwApiUrl}{endpoint}/{id}/");
            
            return JsonConvert.DeserializeObject<T>(json);
        }

        static async Task<PaginatedResultsEntity<T>> GetAllPaginatedAsync<T>(string endpoint, string page = "1") where T : IBaseEntity
        {
            var json = await ApiRequestAsync($"{SwApiUrl}{endpoint}?page={page}");
            
            return JsonConvert.DeserializeObject<PaginatedResultsEntity<T>>(json);
        }

        static async Task<string> ApiRequestAsync(string url)
        {
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            using (var response = await HttpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
