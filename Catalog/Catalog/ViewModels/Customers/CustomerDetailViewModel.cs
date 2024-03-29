﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Behaviour;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Locations;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Views.Customers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Catalog.ViewModels.Customers
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ILocationService _locationService;
        private readonly INetworkService _networkService;
        private readonly UnitOfWork _unitOfWork;

        public CustomerDetailViewModel(
            Customer customer,
            IDialogService dialogService, 
            ILocationService locationService, 
            INetworkService networkService, 
            INavigationService navigationService, 
            UnitOfWork unitOfWork) 
            : base(navigationService)
        {
            Customer = customer;
            _dialogService = dialogService;
            _locationService = locationService;
            _networkService = networkService;
            _unitOfWork = unitOfWork;

            Pins = new ObservableCollection<Pin>();
        }

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                RaisePropertyChanged(() => Customer);
            }
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

        private bool _isMapVisible;
        public bool IsMapVisible
        {
            get => _isMapVisible;
            set => Set(ref _isMapVisible, value);
        }

        public MoveToRegionRequest Request { get; } = new MoveToRegionRequest();

        public ICommand ChangeCustomerCommand => new Command(async () => await ChangeCustomerCommandExecute());

        private async Task ChangeCustomerCommandExecute()
        {
            if (Customer == null)
            {
                return;
            }

            await NavigationService.NavigateToAsync<NewCustomerPage, ChangeCustomerViewModel, Customer>(Customer, false);
        }

        public ICommand RemoveCustomerCommand => new Command(async () => await RemoveCustomerCommandExecute());

        private async Task RemoveCustomerCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            if (Customer.Orders.Count > 0)
            {
                await _dialogService.Alert($"Вы не можете удалить {Title}.");
                return;
            }

            bool result = await _dialogService.Confirm($"Вы подтверждаете удаление {Title}?");
            if (!result)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _unitOfWork.CustomerRepository.Remove(Customer);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
                await NavigationService.NavigateBackToMainPageAsync();
            }
        }

        protected override async void AppearingCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Customer = _unitOfWork.CustomerRepository.GetById(Customer.Id);
                Title = Customer.Name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            if (!_networkService.IsInternetConnected)
            {
                await _dialogService.Alert("Отсутвует соедиение с интернетом! Карта не будет доступна!", "Ошибка");
                return;
            }

            IsMapVisible = false;
            IsBusy = true;

            try
            {
                var position = await _locationService.SearchPositionForAddressAsync(Customer.Address);
                var pin = _locationService.CreatePin(position, Customer.Address);

                Request.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(2)));
                Pins.Clear();
                Pins.Add(pin);

                IsMapVisible = true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
