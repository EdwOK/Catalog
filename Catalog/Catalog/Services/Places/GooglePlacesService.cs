using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.Infrastructure.Extensions;
using Newtonsoft.Json;

namespace Catalog.Services.Places
{
    public class GooglePlacesService : IGooglePlacesService
    {
        public async Task<AutoCompleteResult> GetAutoCompletePlaces(AutoCompleteRequest request)
        {
            var query = CreateQuery(request);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(query, new StringContent("")).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var autoCompleteResult = JsonConvert.DeserializeObject<AutoCompleteResult>(responseData);
                    return autoCompleteResult;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                throw;
            }
        }

        public async Task<Prediction> GetFirstAutoCompletePlace(AutoCompleteRequest request)
        {
            var autoCompleteResult = await GetAutoCompletePlaces(request);
            return autoCompleteResult.Predictions.FirstOrDefault();
        }

        private Uri CreateQuery(AutoCompleteRequest request)
        {
            var uri = new Uri(GooglePlacesConfigs.ApiUrl)
                .AddQueryParams(request);

            return uri;
        }
    }
}
