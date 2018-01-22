using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace RadioFileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: RadioFileCopy.exe sourceDir destDir");
                return;
            }

            FileInfo [] sourceFiles = new DirectoryInfo(args[0]).GetFiles();
            FileInfo[] destFiles = new DirectoryInfo(args[1]).GetFiles();
            string destFile = destFiles[destFiles.Length - 1].Name;

            for (int i = sourceFiles.Length - 1; i >= 0; i--)
            {
                if (sourceFiles[i].Name != destFile)
                {
                    FileSystem.CopyFile(sourceFiles[i].FullName, args[1]);
                }
            }
        }
    }
}
