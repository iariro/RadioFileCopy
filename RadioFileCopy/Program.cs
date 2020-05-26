using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace RadioFileCopy
{
	class DriveList
	{
		private readonly string[] drives;

		public DriveList()
		{
			drives = Directory.GetLogicalDrives();
		}

		public string CompleteDriveAndPath(string path)
		{
			foreach (string drive in drives)
			{
				string driveAndPath = Path.Combine(drive, path);
				if (Directory.Exists(driveAndPath))
				{
					// 存在する

					return driveAndPath;
				}
			}

            return null;
		}
	}

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: RadioFileCopy.exe sourceDir destDir");
                return;
            }

			DriveList driveList = new DriveList();
			string sourceDir = driveList.CompleteDriveAndPath(args[0]);
			string destDir = driveList.CompleteDriveAndPath(args[1]);
            int? limitFileNum = null;
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i].StartsWith("/limitfilenum="))
                {
                    limitFileNum = int.Parse(args[i].Split('=')[1]);
                }
            }

            if (sourceDir == null || destDir == null)
            {
                Console.WriteLine("それと思われるパスが見つかりません");
                return;
            }

            try
            {
                FileInfo[] sourceFiles = new DirectoryInfo(sourceDir).GetFiles();
                FileInfo[] destFiles = new DirectoryInfo(destDir).GetFiles();
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
                        FileSystem.CopyFile(sourceFiles[i].FullName, Path.Combine(destDir, sourceFiles[i].Name), UIOption.AllDialogs);
                    }
                    else
                    {
                        break;
                    }
                }

                if (limitFileNum != null)
                {
                    for (int i = limitFileNum.Value; i < sourceFiles.Length; i++)
                    {
                        string path = sourceFiles[sourceFiles.Length - 1 - i].FullName;
                        Console.WriteLine("Deleting " + path);
                        //if (Console.ReadLine() == "y")
                        {
                            File.Delete(path);
                        }
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
