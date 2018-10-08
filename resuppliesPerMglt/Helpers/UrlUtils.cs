using System;
using System.Collections.Specialized;
using System.Web;

namespace ResuppliesPerMglt.Helpers
{
    /// <summary>
    /// Contains helper methods for dealing with URL strings
    /// </summary>
    public class UrlUtils
    {
        const string AnyBaseUrlStr = "http://a.b";

        public static string GetPageNumber(string url) => GetQueryParams(url)["page"];

        /// <summary>
        /// Parses the input url and extracts the query parameters
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryParams(string url)
        {
            var uri = new Uri(url ?? string.Empty, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri) uri = new Uri($"{AnyBaseUrlStr}/{uri.OriginalString}");

            return HttpUtility.ParseQueryString(uri.Query);
        }
    }
}
