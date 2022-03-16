using LE.Cafe.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LE.Cafe.Views.RgPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableList : PopupPage
    {
        public TableList(OrderViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label= sender as Label;
            Expander expander = label.Parent.Parent.Parent.Parent.Parent as Expander;
            if (label.FontSize == Device.GetNamedSize(NamedSize.Default, label))
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
            }
            else
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Default, label);
            }
            expander.ForceUpdateSize();
        }

        private void Expand_Tapped(object sender, EventArgs e)
        {
            var exp = (Expander)sender;
            exp.ForceUpdateSize();
        }
    }
}