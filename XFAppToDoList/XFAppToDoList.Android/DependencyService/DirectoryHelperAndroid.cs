﻿using System;
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
        public string GetDBAddress(string dbName)
        {
            var folder = Android.OS.Environment.ExternalStorageDirectory + JFile.Separator + EFolders.Data;
            var file = new JFile(folder);
            if (!file.Exists())
            {
                file.Mkdir();
                file.Dispose();
            }
            return CombineFolderPath(dbName, EFolders.Data);
        }


        public string CombineFolderPath(string address, EFolders eFolders)
        {
            string checkFolder = (eFolders == 0 ? String.Empty : (JFile.Separator + eFolders.ToString()));
            return Android.OS.Environment.ExternalStorageDirectory + JFile.Separator+ checkFolder + JFile.Separator + address ;
            //throw new NotImplementedException();
        }

        public bool CreateFile(string address, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);
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

        public bool CreateFolder(string address, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);
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

        public bool DeleteFile(string address, EFolders eFolders)
        {
            try
            {
                address= CombineFolderPath(address,eFolders);
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

        public string ReadData(string address, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);

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

        public bool WriteData(string address, string data, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);
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