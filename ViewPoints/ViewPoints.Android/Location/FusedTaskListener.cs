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
    /// <summary>
    /// Třída která od systému zachytí informace o tom že došlo k: 
    /// - připojení ke GoogleApiClient
    /// - selhání pokusu o připojení ke GoogleApiClient
    /// - ukončení spojení s GoogleApiClient
    /// - změnila se poloha uživatele
    /// </summary>
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

        /// <summary>
        /// Umožňuje nastavit libovolný TaskCompletionSource, který umožňuje awaitovat získání polohy
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(TaskCompletionSource<Position> source)
        {
            this.source = source;
        }

        /// <summary>
        /// Metoda která se volá když dojde k navázání spojení s GoogleApiClient
        /// </summary>
        /// <param name="connectionHint"></param>
        public void OnConnected(Bundle connectionHint)
        {
            this.CallConnected();
        }

        /// <summary>
        /// Metoda která se volá když je spojení s GoogleApiClient ukočeno
        /// </summary>
        /// <param name="cause"></param>
        public void OnConnectionSuspended(int cause)
        {
            if (this.source != null)
            {
                source.SetCanceled();
                source = null;
            }
        }

        /// <summary>
        /// Metoda která se volá pokud se nepodaří spojit s GoogleApiClient
        /// </summary>
        /// <param name="result"></param>
        public void OnConnectionFailed(ConnectionResult result)
        {
            if (this.source != null)
            {
                source.SetCanceled();
                source = null;
            }
        }

        /// <summary>
        /// Metoda která se volá, když se změní poloha
        /// </summary>
        /// <param name="location"></param>
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