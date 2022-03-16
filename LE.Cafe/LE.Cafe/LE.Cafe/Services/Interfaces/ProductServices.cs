using LE.Cafe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LE.Cafe.Services.Interfaces
{
   public interface ProductServices
    {
        Task<ObservableCollection<MenuItem>> GetAllMenuItemsAsync(bool forceRefresh = false);
        Task<ObservableCollection<MenuCategory>> GetAllMenuCategoriesAsync(bool forceRefresh = false);
        Task<ObservableCollection<TableCategory>> GetAllTableCategoryAsync(); 
        Task<ObservableCollection<CafeTable>> GetAllCafeTablesAsync();
        Task<bool> PostCheckout(TableOrder order);
    }
}
