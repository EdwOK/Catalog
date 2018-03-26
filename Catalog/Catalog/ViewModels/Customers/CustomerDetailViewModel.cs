using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Behaviour;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Locations;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Catalog.ViewModels.Customers
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly ILocationService _locationService;
        private readonly INetworkService _networkService;

        public CustomerDetailViewModel(
            Customer customer,
            INavigationService navigationService, 
            UnitOfWork unitOfWork,
            IDialogService dialogService, 
            ILocationService locationService, 
            INetworkService networkService)
        {
            Customer = customer;
            Title = customer.Name;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _locationService = locationService;
            _networkService = networkService;

            Pins = new ObservableCollection<Pin>();
        }

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set => Set(ref _customer, value);
        }

        private ObservableCollection<Pin> _pins;
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => Set(ref _pins, value);
        }

        private MapSpan _visibleRegion;
        public MapSpan VisibleRegion
        {
            get => _visibleRegion;
            set => Set(ref _visibleRegion, value);
        }

        private bool _mapIsVisible;
        public bool MapIsVisible
        {
            get => _mapIsVisible;
            set => Set(ref _mapIsVisible, value);
        }

        public MoveToRegionRequest Request { get; } = new MoveToRegionRequest();

        protected override async void AppearingCommandExecute()
        {
            if (!_networkService.IsInternetConnected)
            {
                await _dialogService.Alert("Отсутвует соедиение с интернетом! Карта не будет доступна!", "Ошибка");
                return;
            }

            try
            {
                var position = await _locationService.SearchPositionForAddressAsync(Customer.Address);
                var pin = _locationService.CreatePin(position, Customer.Address);

                Request.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(2)));
                Pins.Add(pin);

                MapIsVisible = true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }
    }
}
