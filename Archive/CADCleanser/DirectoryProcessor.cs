using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADCleanser;

public class DirectoryProcessor
{
    private readonly IFileProcessor _fileProcessor;
    private readonly ILogger<DirectoryProcessor> _logger;

    public DirectoryProcessor(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }

    public void ProcessDirectory(string directoryPath)
    {
        int i = 0;
        var files = Directory.GetFiles(directoryPath, "*.sld*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            if (i > 5)
            {
                break;
            }

            // Skip temporary files that start with '~$'
            if (Path.GetFileName(file).StartsWith("~$"))
            {
                continue;
            }

            Console.WriteLine($"Processing file: {file}");
            _fileProcessor.ProcessFile(file);
            i++;
        }
    }
}