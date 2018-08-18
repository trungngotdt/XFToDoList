using System;
using System.Collections.Generic;
using System.Text;

namespace XFAppToDoList.MyUtilities
{
    public interface IDirectoryHelper
    {
        bool CreateFolder(string address);
        bool CreateFile(string address);
        bool DeleteFile(string address);
        bool WriteData(string address,string data);
        string ReadData(string address);
    }
}
