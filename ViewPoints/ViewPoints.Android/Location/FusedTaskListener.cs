using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ViewPoints.Backend.Models;

namespace ViewPoints.Droid.Location
{
    class FusedTaskListener : Java.Lang.Object, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener
    {
        /// <summary>
        /// Událost která se volá, pokud dojde k připojení k GooglePlayAPI
        /// </summary>
        public event EventHandler Connected;

        private void CallConnected()
        {
            if (this.Connected != null)
                this.Connected(this, EventArgs.Empty);
        }

        private TaskCompletionSource<Position> source;

        public FusedTaskListener()
        {

        }

        public void SetSource(TaskCompletionSource<Position> source)
        {
            this.source = source;
        }

        public void OnConnected(Bundle connectionHint)
        {
            this.CallConnected();
        }

        public void OnConnectionSuspended(int cause)
        {
            if (this.source != null)
            {
                source.SetCanceled();
                source = null;
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (this.source != null)
            {
                source.SetCanceled();
                source = null;
            }
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            if (this.source != null)
            {
                this.source.SetResult(new Position()
                {
                    Latitude = (float)location.Latitude,
                    Longitude = (float)location.Longitude,
                    Altitude = (float)location.Altitude
                });
                this.source = null;
            }
        }
    }
}