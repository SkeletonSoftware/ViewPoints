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
    public class LocationManager : ILocationManager
    {
        public async Task<Position> GetLocation()
        {
            //Vytvoření listeneru
            var locationListener = new FusedTaskListener();
            var locationSource = new TaskCompletionSource<Position>();
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
            output = await locationSource.Task;
            locationListener.Connected -= handler; //Pokud ještě nedošlo k navázání spojení
                                                   //If by měl zaručit že aplikace na všech zařízeních skutečně počká než se await dokončí.
            if (output != null)
            {
                await this.StopFusedLocation(apiClient, locationListener);
            }
            else
            {
                await this.StopFusedLocation(apiClient, locationListener);
            }
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