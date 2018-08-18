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
using JFile = Java.IO.File;
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
            try
            {
                address = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + address;
                JFile file = new JFile(address);
                if (!file.Exists())
                {
                    var result= file.CreateNewFile();
                    file.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        public bool CreateFolder(string address)
        {
            try
            {
                address = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + address;
                JFile file = new JFile(address);
                if (!file.Exists())
                {
                    var result= file.Mkdir();
                    file.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            return false;
        }

        public bool DeleteFile(string address)
        {
            try
            {
                JFile file = new JFile(address);
                if (file.Exists())
                {
                    var result= file.Delete();
                    file.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
            //throw new NotImplementedException();
        }

        public string ReadData(string address)
        {
            try
            {
                address = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + address;

                JFile file = new JFile(address);
                if (file.IsFile && file.CanWrite())
                {
                    Java.IO.BufferedReader br = new Java.IO.BufferedReader(new Java.IO.FileReader(file));
                    StringBuilder str=new StringBuilder();
                    string temp="";
                    while ((temp=br.ReadLine() )!= null)
                    {
                        str.Append(temp);
                    }
                    br.Dispose();
                    return str.ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
            //throw new NotImplementedException();
        }

        public bool WriteData(string address, string data)
        {
            try
            {
                address = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + address;
                JFile file = new JFile(address);
                if (file.IsFile&&file.CanWrite())
                {
                    Java.IO.FileWriter writer = new Java.IO.FileWriter(file);
                    writer.Write(data);
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
            //throw new NotImplementedException();
        }
    }
}