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
                if(!Path.IsPathFullyQualified(filePath))
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
                var dirPath = args[1];
                var fileType = args[2];
                Console.WriteLine($"Directory {dirPath} selected for {fileType} files.");

            }
            else
            {
                Console.WriteLine($"Invalid command line options.");
            }

            Console.WriteLine($"Press Enter to quit.");
            Console.ReadLine();
        }

        private static void ProcessSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }
    }
}
