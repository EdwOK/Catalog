using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Catalog.Services.Locations
{
    public interface ILocationService
    {
        Task<Pin> GetPinForAddress(string address);

        Task<Pin> GetPinForPosition(Position position);

        Pin CreatePin(Position position, string address);

        Task<Position> SearchPositionForAddressAsync(string address);

        Task<string> SearchAddressForPositionAsync(Position position);

        Task<bool> IsAddressExists(string address);
    }
}
