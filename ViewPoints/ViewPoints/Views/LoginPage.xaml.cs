using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ViewPoints.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var location = DependencyService.Get<ILocationManager>();
            var loc = await location.GetLocation();
        }
    }
}