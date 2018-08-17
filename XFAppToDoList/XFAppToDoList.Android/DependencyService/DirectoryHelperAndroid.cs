using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XFAppToDoList.MyUtilities;
using Xamarin.Forms;
using XFAppToDoList.Droid.DependencyService;

[assembly: Dependency(typeof(DirectoryHelperAndroid))]
namespace XFAppToDoList.Droid.DependencyService
{
    public class DirectoryHelperAndroid : IDirectoryHelper
    {
        public bool CreateFile(string address)
        {
            throw new NotImplementedException();
        }

        public bool CreateFolder(string address)
        {
            var s= Android.OS.Environment.DataDirectory.ToPath().ToString();
            string documentBasePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            DirectoryInfo directory = new DirectoryInfo(documentBasePath);
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                
                //dir.Delete(true);
            }
            //Android.OS.Environment..GetFolderPath(Android.OS.Environment.SpecialFolder.Personal)
            throw new NotImplementedException();
        }

        public bool DeleteFile(string address)
        {
            throw new NotImplementedException();
        }

        public string ReadData(string address)
        {
            throw new NotImplementedException();
        }

        public bool WriteData(string address)
        {
            throw new NotImplementedException();
        }
    }
}