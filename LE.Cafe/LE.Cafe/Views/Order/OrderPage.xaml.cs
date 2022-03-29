using LE.Cafe.ViewModels;
using LE.Cafe.Views.RgPopup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LE.Cafe.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {

        public OrderPage()
        {
            InitializeComponent();
            BindingContext = App.ServiceProvider.GetService<OrderViewModel>();
            /// CategoriesList.IsVisible = false;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label entry = (sender as Label);
            //inputChoice.Text = entry.Text;
           // CategoriesList.IsVisible = false;
        }

        //private void Frame_Focused(object sender, FocusEventArgs e)
        //{
        //    //CategoriesList.IsVisible = true;
        //    Device.InvokeOnMainThreadAsync(async () => 
        //    await PopupNavigation.Instance.PushAsync(new TableList())
        //    );
        //}
    }
}