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
        public bool CreateFile(string address)
        {
            try
            {
                address =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"/"+address;
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

        public bool CreateFolder(string address)
        {
            try
            {
                if (!Directory.Exists(address))
                {
                    var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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

        public bool DeleteFile(string address)
        {
            try
            {
                address = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/" + address;
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

        public string ReadData(string address)
        {
            try
            {
                address = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/" + address;
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

        public bool WriteData(string address, string data)
        {
            try
            {
                address = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/" + address;
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
    }
}
