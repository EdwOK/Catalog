using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace Catalog.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly Geocoder _geocoder;

        public LocationService(Geocoder geocoder)
        {
            _geocoder = geocoder;
        }

        public async Task<Pin> GetPinForAddress(string address)
        {
            var searchPosition = await SearchPositionForAddressAsync(address);
            var pin = CreatePin(searchPosition, address);
            return pin;
        }

        public async Task<Pin> GetPinForPosition(Position position)
        {
            var address = await SearchAddressForPositionAsync(position);
            var pin = CreatePin(position, address);
            return pin;
        }

        public Pin CreatePin(Position position, string address)
        {
            var pin = new Pin
            {
                Type = PinType.SearchResult,
                Position = position,
                Label = address,
                Address = address
            };

            return pin;
        }

        public async Task<Position> SearchPositionForAddressAsync(string address)
        {
            try
            {
                var positions = await _geocoder.GetPositionsForAddressAsync(address).ConfigureAwait(false);
                return positions.FirstOrDefault();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                throw;
            }
        }

        public async Task<string> SearchAddressForPositionAsync(Position position)
        {
            try
            {
                var addresses = await _geocoder.GetAddressesForPositionAsync(position).ConfigureAwait(false);
                return addresses.FirstOrDefault();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                throw;
            }
        }

        public async Task<bool> IsAddressExists(string address)
        {
            var searchPosition = await SearchPositionForAddressAsync(address);
            return searchPosition != default(Position);
        }
    }
}
