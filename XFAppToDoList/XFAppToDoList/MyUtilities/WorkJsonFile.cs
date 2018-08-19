using System;
using System.Collections.Generic;
using System.Text;
using XDependencyService = Xamarin.Forms.DependencyService;
using XFAppToDoList.MyUtilities;

namespace XFAppToDoList.MyUtilities
{
    public class WorkJsonFile : IWorkJsonFile
    {
        public bool LoadJsonFile(string nameOfFile)
        {
            //XDependencyService.Get<IDirectoryHelper>().ReadData("");
            throw new NotImplementedException();
        }

        public bool SaveJsonFile(string jsonString)
        {
            throw new NotImplementedException();
        }
    }
}
