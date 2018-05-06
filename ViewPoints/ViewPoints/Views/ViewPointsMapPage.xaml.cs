using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.Models;
using ViewPoints.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ViewPoints.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPointsMapPage : ContentPage
    {
        public ViewPointsMapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locationManager = DependencyService.Get<ILocationManager>();
            Backend.Models.Position location = null;
            location = await locationManager.GetLocation(); //ziskani pozice uzivatele
            map.IsShowingUser = true;//zobrazi pozici uzivatele na mape
            if (location != null)
            {
                var center = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude); //převod na Formsovou pozici
                map.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromKilometers(5)));//přesun mapy na pozici uživatele a zobrazení 5km okolí
            }


        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();
            map.IsShowingUser = false; //vypne zobrazovani uzivatele na mape. Je nutne volat, jinak aplikace bude spotrebovavat baterii i na pozadi
        }
    }
}