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

            try
            {
                FileInfo[] sourceFiles = new DirectoryInfo(args[0]).GetFiles();
                FileInfo[] destFiles = new DirectoryInfo(args[1]).GetFiles();
                Array.Sort<FileInfo>(
                    sourceFiles,
                    delegate(FileInfo a, FileInfo b)
                    {
                        // ファイル名でソート
                        return a.Name.CompareTo(b.Name);
                    });

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

                    if (!find)
                    {
                        Console.WriteLine(string.Format("Copying {0}", sourceFiles[i].Name));
                        FileSystem.CopyFile(sourceFiles[i].FullName, Path.Combine(args[1], sourceFiles[i].Name), UIOption.AllDialogs);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
