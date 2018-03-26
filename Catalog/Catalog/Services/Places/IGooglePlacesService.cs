using System.Threading.Tasks;

namespace Catalog.Services.Places
{
    public interface IGooglePlacesService
    {
        Task<AutoCompleteResult> GetAutoCompletePlaces(AutoCompleteRequest request);
    }
}
