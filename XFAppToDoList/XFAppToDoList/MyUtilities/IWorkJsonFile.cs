using System;
using System.Collections.Generic;
using System.Text;

namespace XFAppToDoList.MyUtilities
{
    public interface IWorkJsonFile
    {
        bool SaveJsonFile(string jsonString);

        bool LoadJsonFile(string nameOfFile);
    }
}
