using System;
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
            throw new NotImplementedException();
        }

        public bool CreateFolder(string address)
        {
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
