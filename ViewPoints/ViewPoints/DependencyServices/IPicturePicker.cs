using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewPoints.DependencyServices
{
    public interface IPicturePicker
    {
        Task<byte[]> GetPictureFromCamera();
        Task<byte[]> GetPictureFromGallery();
    }
}
