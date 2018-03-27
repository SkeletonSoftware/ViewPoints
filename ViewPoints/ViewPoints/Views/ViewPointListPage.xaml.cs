using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ViewPoints.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPointListPage : ContentPage
	{
        private ViewPointListViewModel viewModel;

        public ViewPointListPage ()
		{
			InitializeComponent ();
            this.viewModel = new ViewPointListViewModel();
            this.BindingContext = this.viewModel;
		}

        private void Add_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddViewPointPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.viewModel.LoadData();
        }
    }
}