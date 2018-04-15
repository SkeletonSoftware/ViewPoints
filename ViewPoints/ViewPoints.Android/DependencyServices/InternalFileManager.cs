using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ViewPoints.DependencyServices;
using ViewPoints.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternalFileManager))]
namespace ViewPoints.Droid.DependencyServices
{
    public class InternalFileManager : IInternalFileManager
    {
        public async Task Delete(string fileName)
        {

            await Task.Run(() =>
            {
                File.Delete(GetAbsolutePath(fileName));
            });
        }

        public bool DirectoryExists(string directoryName)
        {
            return Directory.Exists(GetAbsolutePath(directoryName));
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetAbsolutePath(fileName));
        }

        public async Task<byte[]> GetBytes(string fileName)
        {
            var info = new FileInfo(GetAbsolutePath(fileName));
            var length = info.Length;
            byte[] output;
            using (var reader = File.OpenRead(GetAbsolutePath(fileName)))
            {
                output = new byte[length];
                await reader.ReadAsync(output, 0, output.Length);
            }
            return output;
        }

        public async Task WriteBytes(string fileName, byte[] data)
        {

            using (var writer = File.OpenWrite(GetAbsolutePath(fileName)))
            {
                await writer.WriteAsync(data, 0, data.Length);
            }
        }

        public string GetAbsolutePath(string fileName)
        {
            return Path.Combine(Android.App.Application.Context.FilesDir.AbsolutePath, fileName);
        }
    }
}