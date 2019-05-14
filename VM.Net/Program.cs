using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VM.Net.Commands;

namespace VM.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileAttributes = File.GetAttributes(args[0]);
            var fileDirectoryName = Path.GetDirectoryName(args[0]);
            var filePaths = new List<string>();
            var assemblyProgramFilePathWithoutExtension = string.Empty;

            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                assemblyProgramFilePathWithoutExtension = $"{args[0]}\\{args[0].Split("\\").Last()}";
                filePaths.AddRange(Directory.GetFiles(args[0], "*.vm"));
            }
            else
            {
                assemblyProgramFilePathWithoutExtension = $"{fileDirectoryName}\\{fileDirectoryName.Split("\\").Last()}";
                filePaths.Add(args[0]);
            }

            //Sys.vm is required to be the first file executed
            filePaths = filePaths.OrderBy(filePath => filePath, new FilePathComparer()).ToList();

            Console.WriteLine("The following files will be read:");
            Console.WriteLine(string.Join(Environment.NewLine, filePaths));

            var commandFactory = new CommandFactory();
            var parser = new Parser(commandFactory);
            var stack = new Stack();
            var commands = new List<Command>();
            var bootstrap = new Bootstrap();

            foreach (var filePath in filePaths)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string[] lines = File.ReadAllLines(filePath);

                commands.AddRange(parser.ParseVMCommands(
                    new VMCommandsContext
                    {
                        FileName = fileNameWithoutExtension
                    }, 
                    lines
                ));
            }

            Console.WriteLine($"{commands.Count()} commands found.");

            var assemblyInstructions = bootstrap.GenerateAssemblyInstructions();

            assemblyInstructions = commands.Aggregate(assemblyInstructions, (a, c) => 
            {
                a.AddRange(c.Execute(stack));
                return a;
            });

            Console.WriteLine($"{assemblyInstructions.Count()} assembly instructions generated.");

            File.WriteAllLines($"{assemblyProgramFilePathWithoutExtension}.asm", assemblyInstructions);
        }


        public class Parser
        {
            private readonly CommandFactory _commandFactory;

            public Parser(CommandFactory commandFactory)
            {
                _commandFactory = commandFactory;
            }

            public IEnumerable<Command> ParseVMCommands(VMCommandsContext context, string[] lines)
            {
                var remainingLines = StripComments(lines);

                return remainingLines.Select(line => _commandFactory.Create(context, line));
            }

            private IList<string> StripComments(string[] lines)
            {
                var remainingLines = new List<string>();

                for (int i = 0; i < lines.Length; i++)
                {
                    var assemblyInstruction = GetNonComment(lines[i]);

                    if (string.IsNullOrWhiteSpace(assemblyInstruction))
                    {
                        continue;
                    }

                    remainingLines.Add(assemblyInstruction);
                }

                return remainingLines;
            }

            private string GetNonComment(string line)
            {
                var indexOfComment = line.IndexOf("//");

                if (indexOfComment > -1)
                {
                    line = line.Substring(0, Math.Max(0, indexOfComment));
                }

                return line.Trim();
            }
        }
    }
}
