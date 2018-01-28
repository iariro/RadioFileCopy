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
                bool find = false;
                for (int j = 0; j < destFiles.Length; j++)
                {
                    if (sourceFiles[i].Name == destFiles[j].Name)
                    {
                        find = true;
                        break;
                    }
                }
                System.Console.WriteLine(sourceFiles[i].Name + " " + find);

                if (!find)
                {
                    FileSystem.CopyFile(sourceFiles[i].FullName, args[1]);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
