using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using ViewPoints.DependencyServices;
using ViewPoints.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePicker))]

namespace ViewPoints.Droid.DependencyServices
{
    public class PicturePicker : IPicturePicker
    {
        public Task<byte[]> GetPictureFromCamera()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            var file = new File(GetDirectoryForPictures(), String.Format("photo_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
            MainActivity.Instance.StartActivityForResult(intent, MainActivity.PickImageCameraId);
            MainActivity.Instance.ImageFile = file;

            MainActivity.Instance.ImageTaskCompletionSource = new TaskCompletionSource<byte[]>();


            // Return Task object
            return MainActivity.Instance.ImageTaskCompletionSource.Task;
        }

        public Task<byte[]> GetPictureFromGallery()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageGalleryId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.ImageTaskCompletionSource = new TaskCompletionSource<byte[]>();

            // Return Task object
            return MainActivity.Instance.ImageTaskCompletionSource.Task;
        }

        private File GetDirectoryForPictures()
        {
            var dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "ViewPoints");
            if (!dir.Exists())
            {
                dir.Mkdirs();
            }
            return dir;
        }
    }
}