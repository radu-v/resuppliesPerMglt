using System.Collections.Generic;

namespace ResuppliesPerMglt.Models
{
    public class PaginatedResultsEntity<T> : IBaseEntity where T : IBaseEntity
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public IList<T> Results { get; set; }
    }
}
