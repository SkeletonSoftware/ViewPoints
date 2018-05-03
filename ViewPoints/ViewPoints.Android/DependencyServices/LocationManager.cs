using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ViewPoints.Backend.Models;
using ViewPoints.DependencyServices;
using ViewPoints.Droid.Location;
using Xamarin.Forms;

[assembly: Dependency(typeof(ViewPoints.Droid.DependencyServices.LocationManager))]
namespace ViewPoints.Droid.DependencyServices
{
    /// <summary>
    /// Třída pro práci s polohou na Androidu
    /// </summary>
    public class LocationManager : ILocationManager
    {
        /// <summary>
        /// Metoda která vrátí aktální polohu zařízení, pomocí fused location
        /// </summary>
        /// <returns></returns>
        public async Task<Position> GetLocation()
        {
            //Vytvoření listeneru
            var locationListener = new FusedTaskListener();
            var locationSource = new TaskCompletionSource<Position>();
            await locationSource.Task;
            locationListener.SetSource(locationSource);

            Position output = null;
            var apiClient = new GoogleApiClient.Builder(Android.App.Application.Context, locationListener, locationListener).AddApi(LocationServices.API).Build();
            //Handler který se zavolá když dojde k napojení na API
            EventHandler handler = null;
            handler = async (sender, e) =>
            {
                locationListener.Connected -= handler;
                if (apiClient != null && apiClient.IsConnected)
                {
                    var locationRequest = new LocationRequest();
                    locationRequest.SetNumUpdates(1);
                    await LocationServices.FusedLocationApi.RequestLocationUpdatesAsync(apiClient, locationRequest, locationListener);
                }
            };

            //Zahájení komunikace s API
            locationListener.Connected += handler;
            apiClient.Connect();

            //Čekání na získání polohy
            output = await locationSource.Task;

            //Ukončení komunikace s klientem 
            locationListener.Connected -= handler; 
            await this.StopFusedLocation(apiClient, locationListener);
            apiClient.Dispose();
            apiClient = null;
            return output;
        }

        /// <summary>
        /// Zastaví získávání polohy
        /// </summary>
        /// <param name="client"></param>
        /// <param name="listener"></param>
        /// <returns></returns>
        private async Task StopFusedLocation(GoogleApiClient client, FusedTaskListener listener)
        {
            if (client.IsConnected)
            {
                await LocationServices.FusedLocationApi.RemoveLocationUpdatesAsync(client, listener);
                client.Disconnect();
            }
        }
    }
}