using System;
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
using Catalog.Views.Employees;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Catalog.ViewModels.Employees
{
    public class EmployeeDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly ILocationService _locationService;
        private readonly INetworkService _networkService;

        public EmployeeDetailViewModel(
            Employee employee, 
            UnitOfWork unitOfWork, 
            INavigationService navigationService,
            IDialogService dialogService, 
            ILocationService locationService, 
            INetworkService networkService)
        {
            Employee = employee;
            Title = employee.FullName;
            _unitOfWork = unitOfWork;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _locationService = locationService;
            _networkService = networkService;

            Pins = new ObservableCollection<Pin>();
        }

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => Set(ref _employee, value);
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

        public ICommand ChangeEmployeeCommand => new Command(async () => await ChangeEmployeeCommandExecute());

        private async Task ChangeEmployeeCommandExecute()
        {
            if (Employee == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<NewEmployeePage, ChangeEmployeeViewModel, Employee>(Employee, false);
        }

        public ICommand RemoveEmployeeCommand => new Command(async () => await RemoveEmployeeCommandExecute());

        private async Task RemoveEmployeeCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            if (Employee.Orders.Count > 0)
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
                _unitOfWork.EmployeeRepository.Remove(Employee);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
                await _navigationService.NavigateBackAsync(false);
            }
        }

        protected override async void AppearingCommandExecute()
        {
            if (!_networkService.IsInternetConnected)
            {
                await _dialogService.Alert("Отсутвует соедиение с интернетом! Карта не будет доступна!", "Ошибка");
                return;
            }

            IsMapVisible = false;

            try
            {
                var position = await _locationService.SearchPositionForAddressAsync(Employee.Address);
                var pin = _locationService.CreatePin(position, Employee.Address);

                Request.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(2)));
                Pins.Add(pin);

                IsMapVisible = true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }
    }
}
