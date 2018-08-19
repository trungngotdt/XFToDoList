using System;
using System.Collections.Generic;
using System.Text;

namespace XFAppToDoList.MyUtilities
{
    public interface IDirectoryHelper
    {
        /// <summary>
        /// Create a folder and address is path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool CreateFolder(string address, EFolders eFolders);
        /// <summary>
        /// Create a empty file and address is path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool CreateFile(string address, EFolders eFolders);
        /// <summary>
        /// Delete a file and address is path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool DeleteFile(string address, EFolders eFolders);
        /// <summary>
        /// Write data to file and address is path
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool WriteData(string address,string data, EFolders eFolders);
        /// <summary>
        /// Read all data in file and address is path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        string ReadData(string address, EFolders eFolders);
        /// <summary>
        /// Combine path
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        string CombineFolderPath(string address, EFolders eFolders);
    }
}
