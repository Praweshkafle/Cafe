using LE.Cafe.Models;
using LE.Cafe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LE.Cafe.Services.Implementation
{
    public class ProductServicesImpl : ProductServices
    {
        private readonly ApiServices _apiServices;
        private readonly string baseUrl= Preferences.Get("url", "leadingedge");
        private readonly string token= Preferences.Get("token", "LeadingEdge");
        public ProductServicesImpl(ApiServices apiServices)
        {
            this._apiServices = apiServices;
        }

        public async Task<ObservableCollection<CafeTable>> GetAllCafeTablesAsync()
        {
            var Url = new Uri(baseUrl);
            var relativeUri = "api/cafetable";
            var fullUrl = new Uri(Url, relativeUri);
            return await _apiServices.GetResultDataAsync<CafeTable>($"{fullUrl}" + "/all", token);
        }

        public async Task<ObservableCollection<MenuCategory>> GetAllMenuCategoriesAsync(bool forceRefresh = false)
        {
            var Url = new Uri(baseUrl);
            var relativeUri = "api/menucategory";
            var fullUrl = new Uri(Url,relativeUri);
            return await _apiServices.GetResultDataAsync<MenuCategory>($"{fullUrl}" + "/all", token);
        }

        public async Task<ObservableCollection<MenuItem>> GetAllMenuItemsAsync(bool forceRefresh = false)
        {
            var Url = new Uri(baseUrl);
            var relativeUri = "api/menu-item";
            var fullUrl = new Uri(Url, relativeUri);
            return await _apiServices.GetResultDataAsync<MenuItem>($"{fullUrl}" + "/all", token);
        }

        public async Task<ObservableCollection<TableCategory>> GetAllTableCategoryAsync()
        {
            var Url = new Uri(baseUrl);
            var relativeUri = "api/tablecategory";
            var fullUrl = new Uri(Url, relativeUri);
            return await _apiServices.GetResultDataAsync<TableCategory>($"{fullUrl}" + "/all", token);
        }

        public async Task<bool> PostCheckout(TableOrder order)
        {
            var Url = new Uri(baseUrl);
            var relativeUri = "/restaurant/billing/saveorders";
            var fullUrl = new Uri(Url, relativeUri);
            return await _apiServices.PostDataAsync<TableOrder>($"{fullUrl}",order);
        }
    }
}
