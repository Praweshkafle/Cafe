using LE.Cafe.Models;
using LE.Cafe.Services.Interfaces;
using LE.Cafe.Views.RgPopup;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LE.Cafe.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private ObservableCollection<string> urlList = new ObservableCollection<string>
        {
            "https://cafe.samparkah.com/","abcd"
        };
        public ObservableCollection<string> UrlList
        {
            get { return urlList; }
            set { urlList = value; OnPropertyChanged(nameof(UrlList)); }
        }
        
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string _selectedUrl;

        public string SelectedUrl
        {
            get { return _selectedUrl; }
            set { _selectedUrl = value; OnPropertyChanged(nameof(SelectedUrl)); }
        }
        private readonly ApiServices _apiServices;
        public AsyncCommand<object> LoginCommand { get;}
        async Task Login(object obj)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopup());
                Preferences.Clear();
                var response = await _apiServices.PostAsync<User>(SelectedUrl, "api/account/login", UserName, Password);
                if ((response?.token) != null)
                {
                    Preferences.Set("url", SelectedUrl);
                    Preferences.Set("token", response.token);
                    Preferences.Set("name", response.organisation_detail.name);
                    Preferences.Set("address", response.organisation_detail.address);
                    await Device.InvokeOnMainThreadAsync(() => { Application.Current.MainPage = new AppShell(); });
                }
                else
                    await App.Current.MainPage.DisplayAlert("ERROR", "Incorrect Password !!", "ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await PopupNavigation.Instance.PopAsync();
            }
           
        }
        public LoginViewModel(ApiServices apiServices)
        {
            LoginCommand= new AsyncCommand<object>((obj)=>Login(obj),allowsMultipleExecutions:false);
            _apiServices = apiServices;
        }
    }
}
