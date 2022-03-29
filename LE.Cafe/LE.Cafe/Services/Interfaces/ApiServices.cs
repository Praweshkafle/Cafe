using LE.Cafe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LE.Cafe.Services.Interfaces
{
    public interface ApiServices
    {
        Task<T> GetSingleResultDataAsync<T>(string uri, bool forceRefresh = false);
        Task<T> PostAsync<T>(string baseUrl,string uri, string username, string password);
        Task<ObservableCollection<T>> GetDataAsync<T>(string uri, string token = "");
        Task<ObservableCollection<T>> GetResultDataAsync<T>(string url,string token, bool forceRefresh = false);
        Task<bool> PostDataAsync<T>(string baseUrl, TableOrder tableOrder);
    }
}
