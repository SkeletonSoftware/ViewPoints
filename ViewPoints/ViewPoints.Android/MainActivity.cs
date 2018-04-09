using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;
using System.IO;

namespace ViewPoints.Droid
{
    [Activity(Label = "ViewPoints", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly int PickImageGalleryId = 1000;
        public static readonly int PickImageCameraId = 2000;

        public TaskCompletionSource<byte[]> ImageTaskCompletionSource { set; get; }
        public Java.IO.File ImageFile;

        internal static MainActivity Instance;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Instance = this;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageGalleryId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    var ms = new System.IO.MemoryStream();
                    await stream.CopyToAsync(ms);
                    ImageTaskCompletionSource.SetResult(ms.ToArray());
                }
                else
                {
                    ImageTaskCompletionSource.SetResult(null);
                }
            }
            else if (requestCode == PickImageCameraId)
            {
                if ((resultCode == Result.Ok))
                {
                    try
                    {
                        var stream = ContentResolver.OpenInputStream(Android.Net.Uri.FromFile(ImageFile));
                        var ms = new System.IO.MemoryStream();
                        await stream.CopyToAsync(ms);
                        ImageTaskCompletionSource.SetResult(ms.ToArray());
                    }
                    catch (Exception ex)
                    {
                        ImageTaskCompletionSource.SetException(ex);
                    }
                }
                else
                {
                    ImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}

