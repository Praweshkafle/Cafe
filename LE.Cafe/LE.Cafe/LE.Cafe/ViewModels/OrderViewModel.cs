using LE.Cafe.Models;
using LE.Cafe.Services.Interfaces;
using LE.Cafe.Views.RgPopup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LE.Cafe.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly ProductServices _productServices;

        private ObservableCollection<TableCategory> tableCategories = new ObservableCollection<TableCategory>();

        public ObservableCollection<TableCategory> TableCategories
        {
            get { return tableCategories; }
            set { tableCategories = value; OnPropertyChanged(nameof(TableCategories)); }
        }
        // public ObservableCollection<Models.MenuItem> MenuItems { get; set; } = new ObservableCollection<Models.MenuItem>();


        public ObservableCollection<TableCategory> CafeTables { get; set; } = new ObservableCollection<TableCategory>();
        public ObservableCollection<CafeTable> CafeTablesClone { get; set; } = new ObservableCollection<CafeTable>();

        private ObservableCollection<Models.MenuItem> menuItems = new ObservableCollection<Models.MenuItem>();

        public ObservableCollection<Models.MenuItem> MenuItems
        {
            get { return menuItems; }
            set
            {
                if (menuItems != value)
                {
                    menuItems = value;
                    OnPropertyChanged(nameof(MenuItems));
                }
            }
        }


        private bool _categoryIsVisible;
        private TableCategory _oldCategory;

        public bool CategoryIsVisibe
        {
            get { return _categoryIsVisible = false; }
            set { _categoryIsVisible = value; OnPropertyChanged(nameof(CategoryIsVisibe)); }
        }

        private TableCategory _tableCategory;

        public TableCategory TableCategory
        {
            get { return _tableCategory; }
            set
            {
                if (_tableCategory != value)
                {
                    _tableCategory = value;
                    OnPropertyChanged(nameof(TableCategory));
                }
            }
        }

        private Models.MenuItem _selectedMenuItem;

        public Models.MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value; OnPropertyChanged(nameof(SelectedMenuItem)); }
        }


        private decimal _total;

        public decimal Total
        {
            get { return _total; }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private string _selectedMenuItemName;

        public string SelectedMenuItemName
        {
            get { return _selectedMenuItemName; }
            set { _selectedMenuItemName = value; OnPropertyChanged(nameof(SelectedMenuItemName)); }
        }

        private int _quantity = 0;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Total = Rate * _quantity;
                OnPropertyChanged(nameof(Quantity));
            }
        }


        private decimal _item;

        public decimal Rate
        {
            get { return _item; }
            set
            {
                _item = value;
                Total = _item * Quantity;
                OnPropertyChanged(nameof(Rate));
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    _itemName = value;
                    OnPropertyChanged(nameof(ItemName));
                }
            }
        }


        async Task LoadCategories()
        {
            TableCategories.Clear();
            CafeTablesClone.Clear();
            var categories = await _productServices.GetAllTableCategoryAsync();

            foreach (var item in categories.OrderBy(a => a.position))
            {
                TableCategories.Add(new TableCategory
                {
                    table_category_id = item.table_category_id,
                    name = item.name,
                    position = item.position,
                });
            }
            var subCategories = await _productServices.GetAllCafeTablesAsync();
            foreach (var item in subCategories)
            {
                CafeTablesClone.Add(new CafeTable
                {
                    name = item.name,
                    table_category_id = item.table_category_id,
                    table_id = item.table_id,
                    is_reserved = item.is_reserved,
                });
            }
            await LoadMenuItems();
        }

        async Task LoadMenuItems()
        {
            MenuItems.Clear();
            var menuItems = await _productServices.GetAllMenuItemsAsync();
            foreach (var item in menuItems)
            {
                MenuItems.Add(new Models.MenuItem
                {
                    name = item.name,
                    menu_category_id = item.menu_category_id,
                    rate = item.rate,
                    menu_item_id = item.menu_item_id,
                    menu_unit_id = item.menu_unit_id,
                });
            }
        }

        async Task LoadSubCategories(object obj)
        {
            IsBusy = true;
            CafeTables.Clear();
            var tableCategory = (TableCategory)obj;
            HideOrShowCategory(tableCategory);
            var filteredCafeTables = CafeTablesClone.Where(a => a.table_category_id == tableCategory.table_category_id).ToList();
            foreach (var item in filteredCafeTables)
            {
                await Device.InvokeOnMainThreadAsync(() =>
                CafeTables.Add(new TableCategory
                {
                    name = item.name,
                    table_category_id = item.table_id,
                }));
            }
        }
        async Task TableSelected(object obj)
        {
            try
            {
                TableCategory = (TableCategory)obj;
                await PopupNavigation.Instance.PopAsync();
                Title = (TableCategory.name).ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
        async Task DataSelected(object obj)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                SelectedMenuItem = (Models.MenuItem)obj;
                Rate = SelectedMenuItem.rate;
                SelectedMenuItemName = SelectedMenuItem.name;
            });
        }

        async Task Popup()
        {
            await PopupNavigation.Instance.PushAsync(new TableList(this));
        }

        async Task Search()
        {
            try
            {
                string serchedItem = ItemName.Trim();
                var menuItems = await _productServices.GetAllMenuItemsAsync();
                var searchedItems = menuItems.Where(a => a.name.Contains(serchedItem)).ToList();
                MenuItems.Clear();
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (var item in searchedItems)
                    {
                        MenuItems.Add(new Models.MenuItem
                        {
                            name = item.name,
                            menu_category_id = item.menu_category_id,
                            rate = item.rate,
                            menu_item_id = item.menu_item_id,
                            menu_unit_id = item.menu_unit_id,
                        });
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }


        }
        async Task Refresh()
        {
            IsRefreshing = true;
            await Task.Run(async () => await LoadMenuItems());
            IsRefreshing = false;
        }
        private void HideOrShowCategory(TableCategory categoryData)
        {
            if (_oldCategory == categoryData)
            {
                IsExpanded = !IsExpanded;
            }
            else
            {
                if (_oldCategory != null)
                {
                    _oldCategory.IsExpanded = false;
                }
                categoryData.IsExpanded = true;
            }
            _oldCategory = categoryData;
        }

        async Task Checkout()
        {
            var tableOrder = new TableOrder
            {
                delivery_time = DateTime.Now.ToString(),
                menu_item_id = !(SelectedMenuItem.menu_item_id <= 0) ? SelectedMenuItem.menu_item_id : 0,
                menu_rate = Rate >= 1 ? Rate : 0,
                quantity = Quantity >= 1 ? Quantity : 0,
                order_time = DateTime.Now.ToString(),
                table_reservation_id = !(TableCategory.table_category_id <= 0) ? TableCategory.table_category_id : 0,
                item_name = SelectedMenuItem.name != null ? SelectedMenuItem.name : String.Empty,
                totalAmount = Total >= 1 ? Total : 0,
                ready_time = DateTime.Now.ToString(),
            };
            if (tableOrder != null)
            {
                bool response = await _productServices.PostCheckout(tableOrder);
                if (response)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Order Confirmed!!", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid Order!!", "Ok");
            }
            // _productServices
        }

        public AsyncCommand<object> LoadSubCategoriesCommand { get; }
        public AsyncCommand<object> TableSelectedCommand { get; set; }
        public AsyncCommand PopupCommand { get; set; }
        public AsyncCommand<object> SelectedCommand { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand SearchCommand { get; set; }
        public AsyncCommand CheckoutCommand { get; set; }
        public OrderViewModel(ProductServices productServices)
        {
            IsBusy = false;
            this._productServices = productServices;
            Task.Run(async () => await LoadCategories());
            LoadSubCategoriesCommand = new AsyncCommand<object>(async (obj) => await LoadSubCategories(obj), allowsMultipleExecutions: false);
            PopupCommand = new AsyncCommand(async () => await Popup(), allowsMultipleExecutions: false);
            TableSelectedCommand = new AsyncCommand<object>(async (obj) => await TableSelected(obj), allowsMultipleExecutions: false);
            SelectedCommand = new AsyncCommand<object>(async (obj) => await DataSelected(obj), allowsMultipleExecutions: false);
            SearchCommand = new AsyncCommand(async () => await Search(), allowsMultipleExecutions: false);
            RefreshCommand = new AsyncCommand(async () => await Refresh(), allowsMultipleExecutions: false);
            CheckoutCommand = new AsyncCommand(async () => await Checkout(), allowsMultipleExecutions: false);
        }
    }
}
