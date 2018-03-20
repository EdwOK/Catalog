using System;
using System.IO;
using Catalog.Droid;
using Catalog.Infrastructure;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Catalog.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}