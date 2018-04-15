using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewPoints.DependencyServices
{
    public interface IInternalFileManager
    {
        /// <summary>
        /// Zjisti zda soubor existuje
        /// </summary>
        /// <returns></returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Directory zda soubor existuje
        /// </summary>
        /// <returns></returns>
        bool DirectoryExists(string directoryName);

        /// <summary>
        /// Nacte cely soubor jako pole byte
        /// </summary>
        /// <returns>soubor načtený v paměti</returns>
        Task<byte[]> GetBytes(string fileName);

        /// <summary>
        /// Uloží celé pole do souboru
        /// </summary>
        /// <returns></returns>
        Task WriteBytes(string fileName, byte[] data);


        /// <summary>
        /// Smaze soubor
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task Delete(string fileName);

        /// <summary>
        /// Ziska absolutni cestu k souboru
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetAbsolutePath(string fileName);
    }
}
