using System;
using System.IO;

namespace FilesAndStreams
{
    public class FileProcessor
    {

        private const string BackupDirectoryName = "backup";
        private const string InProgressDirectoryName = "processing";
        private const string CompleteDirectoryName = "complete";

        public string InputFilePath { get; }

        public FileProcessor(string filePath) => InputFilePath = filePath;

        public void Process()
        {
            Console.WriteLine($"Begin process of {InputFilePath}");

            // Check if file exists
            if(!File.Exists(InputFilePath))
            {
                Console.WriteLine($"Error: file {InputFilePath} does not exist.");
                return;
            }

            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            Console.WriteLine($"Root data path is {rootDirectoryPath}.");

            // check if backup dir exist
            string backupDirectoryName = Path.Combine(rootDirectoryPath, BackupDirectoryName);

            if(!Directory.Exists(backupDirectoryName))
            {
                Console.WriteLine($"Create {backupDirectoryName}");
                Directory.CreateDirectory(backupDirectoryName);
            }

        }
    }
}
