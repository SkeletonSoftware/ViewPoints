using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(ViewPoints.Controls.Map), typeof(ViewPoints.Droid.Renderers.MapRenderer))]
namespace ViewPoints.Droid.Renderers
{
    public class MapRenderer : Xamarin.Forms.Maps.Android.MapRenderer
    {

        public MapRenderer(Context context):base(context)
        {
        }

        private bool isDrawn = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                (e.OldElement as ViewPoints.Controls.Map).CustomPins.CollectionChanged -= CustomPins_CollectionChanged;
            }
            (e.NewElement as ViewPoints.Controls.Map).CustomPins.CollectionChanged += CustomPins_CollectionChanged;
        }

        private void CustomPins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.CreateNativePins(this.Element as ViewPoints.Controls.Map);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                this.CreateNativePins(this.Element as ViewPoints.Controls.Map);
                isDrawn = true;
            }
        }

        private void CreateNativePins(ViewPoints.Controls.Map map)
        {
            if (NativeMap != null)
            {
                NativeMap.Clear();
                if (map.CustomPins?.Count > 0)
                {
                    for (int i = 0; i < map.CustomPins.Count; i++)
                    {
                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(map.CustomPins[i].Location.Latitude, map.CustomPins[i].Location.Longitude));
                        marker.SetTitle(map.CustomPins[i].Title);
                        marker.SetSnippet(map.CustomPins[i].Description);

                        float[] hsv = new float[3];
                        Android.Graphics.Color.ColorToHSV(map.CustomPins[i].Color.ToAndroid(), hsv);
                        var bitmap = BitmapDescriptorFactory.DefaultMarker(hsv[0]);
                        marker.SetIcon(bitmap);
                        NativeMap.AddMarker(marker);
                    }
                }
            }
        }
    }
}