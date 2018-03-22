﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Views.Products;
using Xamarin.Forms;

namespace Catalog.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public ProductsViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;

            Products = new ObservableCollection<Product>();
        }

        public ICommand ProductSelectedCommand => new Command(ProductSelectedCommandExecute);

        public ICommand LoadProductsCommand => new Command(LoadProductsCommandExecute);

        public ICommand AddProductCommand => new Command(async () => await AddProductCommandExecute());

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }

        private async void ProductSelectedCommandExecute()
        {
            if (SelectedProduct == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<ProductDetailPage, ProductDetailViewModel, Product>(SelectedProduct, false);
            SelectedProduct = null;
        }

        public override void AppearingCommandExecute()
        {
            LoadProductsCommandExecute();
        }

        private void LoadProductsCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Products = _unitOfWork.ProductRepository.GetAll().ToObservable();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddProductCommandExecute()
        {
            await _navigationService.NavigateToAsync<NewProductPage, NewProductViewModel>(false);
        }
    }
}