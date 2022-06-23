using System;
using System.IO;

namespace FilesAndStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Passsing command line options");

            var command = args[0];
            if (command == "--file")
            {
                var filePath = args[1];
                if (!Path.IsPathFullyQualified(filePath))
                {
                    Console.WriteLine($"Error: path '{filePath}' must be fully qualified.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Single file {filePath} selected.");
                ProcessSingleFile(filePath);
            }
            else if (command == "--dir")
            {
                var directoryPath = args[1];
                var fileType = args[2];
                Console.WriteLine($"Directory {directoryPath} selected for {fileType} files.");
                ProcessDirectory(directoryPath, fileType);
            }
            else
            {
                Console.WriteLine($"Invalid command line options.");
            }

            Console.WriteLine($"Press Enter to quit.");
            Console.ReadLine();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            switch (fileType)
            {
                case "TEXT":
                    string[] txtAllFiles = Directory.GetFiles(directoryPath, "*.txt");
                    foreach (var txtFilePath in txtAllFiles)
                    {
                        var fileProcessor = new FileProcessor(txtFilePath);
                        fileProcessor.Process();
                    }
                    break;
                default:
                    Console.WriteLine($"Error: {fileType} is not support.");
                    break;
            }
        }

        private static void ProcessSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }
    }
}
