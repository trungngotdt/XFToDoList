using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFAppToDoList.MyUtilities;
using XFAppToDoList.UWP.DependencyService;

[assembly: Dependency(typeof(DirectoryHelperUWP))]
namespace XFAppToDoList.UWP.DependencyService
{
    public class DirectoryHelperUWP : IDirectoryHelper
    {
        public string GetDBAddress(string dbName)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), EFolders.Data.ToString());
            if (!Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                directory.Create();
            }
            return CombineFolderPath(dbName, EFolders.Data);
        }

        public bool CreateFile(string address, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);
                if (!File.Exists(address))
                {
                    File.Create(address);
                    return File.Exists(address);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;

            //throw new NotImplementedException();
        }

        public bool CreateFolder(string address, EFolders eFolders)
        {
            try
            {
                var dir= CombineFolderPath(address,eFolders);
                if (!Directory.Exists(dir))
                {
                    DirectoryInfo directory = new DirectoryInfo(dir);
                    directory.Create();
                    
                    return directory.Exists;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;

            //throw new NotImplementedException();
        }

        public bool DeleteFile(string address, EFolders eFolders)
        {
            try
            {
                address = CombineFolderPath(address,eFolders);
                if (!File.Exists(address))
                {
                    File.Delete(address);
                    return !File.Exists(address);
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
                if (!File.Exists(address))
                {
                    string text = File.ReadAllText(address);
                    return text;
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
                if(File.Exists(address))
                {
                    File.WriteAllText(address, data);
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

        public string CombineFolderPath(string address,EFolders eFolders)
        {
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),eFolders==0?"":eFolders.ToString(), address);
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), eFolders == 0 ? String.Empty : eFolders.ToString(), address);
        }
    }
}
