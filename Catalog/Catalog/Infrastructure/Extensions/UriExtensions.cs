using System;
using Newtonsoft.Json;

namespace Catalog.Infrastructure.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddQueryParams(this Uri uri, object obj)
        {
            var jsonQuery = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            string query = "?" + jsonQuery
                .Replace(":", "=").Replace("{", "")
                .Replace("}", "").Replace(",", "&")
                .Replace("\"", "");

            var uriBuilder = new UriBuilder(uri)
            {
                Query = query
            };

            return uriBuilder.Uri;
        }
    }
}
