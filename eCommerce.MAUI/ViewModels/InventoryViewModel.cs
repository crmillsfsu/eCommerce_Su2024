using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public string? Query { get; set; }
        public List<ProductViewModel> Products { 
            get {
                return InventoryServiceProxy.Current.Products.Where(p=>p != null)
                    .Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            } 
        }

        public ProductViewModel? SelectedProduct { get; set; }

        public void Edit()
        {
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct?.Model?.Id ?? 0}");
        }

        public async void Delete()
        {
            await InventoryServiceProxy.Current.Delete(SelectedProduct?.Model?.Id ?? 0);
            Refresh();
        }

        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            NotifyPropertyChanged(nameof(Products));
        }

        public async void Search()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
