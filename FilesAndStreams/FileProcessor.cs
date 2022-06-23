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
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);

            if(!Directory.Exists(backupDirectoryPath))
            {
                Console.WriteLine($"Create {backupDirectoryPath}");
                Directory.CreateDirectory(backupDirectoryPath);
            }


            // copy file to backup dir
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
            Console.WriteLine($"Copying {InputFilePath} to {backupFilePath}");
            File.Copy(InputFilePath, backupFilePath, true);

            // move to process dir
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            string inProgressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);
            Console.WriteLine($"Moving {InputFilePath} to {inProgressFilePath}");
            File.Move(InputFilePath, inProgressFilePath, true);

            // determine type of file
            string extension = Path.GetExtension(InputFilePath);
            switch(extension)
            {
                case ".txt":
                    ProcessTextFile(inProgressFilePath);
                    break;
                default:
                    Console.WriteLine($"{extension} is an unsupport file type.");
                    break;
            }

            // move file after processing is complete
            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompleteDirectoryName);
            Directory.CreateDirectory(completedDirectoryPath);
            Console.WriteLine($"Moving {inProgressFilePath} to {completedDirectoryPath}");
            //File.Move(inProgressFilePath, Path.Combine(completedDirectoryPath, inputFileName), true);

            string completedFileName = $"{Path.GetFileNameWithoutExtension(inputFileName)}-{Guid.NewGuid()}{extension}";
            completedFileName = Path.ChangeExtension(completedFileName, ".complete");

            var completedFilePath = Path.Combine(completedDirectoryPath, completedFileName);
            File.Move(inProgressFilePath, completedFilePath);

            string inProgressDirectoryPath = Path.GetDirectoryName(inProgressFilePath);
            Console.WriteLine($"Delete {inProgressDirectoryPath}");
            Directory.Delete(inProgressDirectoryPath, true);
        }

        private void ProcessTextFile(string inProgressFilePath)
        {
            Console.WriteLine($"Processing text file {inProgressFilePath}");
        }
    }
}
