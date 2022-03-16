using LE.Cafe.Models;
using LE.Cafe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LE.Cafe.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        private string _orgName;
        private readonly ApiServices _apiServices;
        private readonly ProductServices _productServices;

        public ObservableCollection<MenuCategory> MenuCategories { get; set; }= new ObservableCollection<MenuCategory>();
        public ObservableCollection<MenuItem> MenuItems { get; set; } = new ObservableCollection<MenuItem>();

        public string OrganizationName
        {
            get { return _orgName; }
            set { _orgName = value; OnPropertyChanged(nameof(OrganizationName)); }
        }
        async Task LoadCategories()
        {
            MenuCategories.Clear();
            var categories = await _productServices.GetAllMenuCategoriesAsync();
            foreach (var item in categories)
            {
                MenuCategories.Add(new MenuCategory
                {
                    name=item.name,
                });
            }
        }

        async Task LoadMenuItems()
        {
            MenuItems.Clear();
            var menuItems= await _productServices.GetAllMenuItemsAsync();
            foreach (var item in menuItems)
            {
                MenuItems.Add(new MenuItem
                {
                    name = item.name,
                    rate = item.rate,
                });
            }
        }

        public HomeViewModel(ApiServices apiServices, ProductServices productServices)
        {
            OrganizationName = Preferences.Get("name","Hotel");
            this._apiServices = apiServices;
            this._productServices = productServices;
            Task.Run( async () => { 
                await LoadCategories(); 
                await LoadMenuItems();
            });
        }
    }
}
