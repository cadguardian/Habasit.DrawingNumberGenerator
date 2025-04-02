using System;
using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using CADCleanser.Models;

namespace CADCleanser.Utilities
{
    public static class JsonParser
    {
        public static ImmutableList<FileMetadata> ParseJson(string jsonFilePath)
        {
            try
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                using JsonDocument doc = JsonDocument.Parse(jsonString);

                var fileList = ImmutableList.CreateBuilder<FileMetadata>();
                var root = doc.RootElement.GetProperty("directory_structure");

                ParseDirectory(root, "", fileList);

                return fileList.ToImmutable();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse JSON file.", ex);
            }
        }

        private static void ParseDirectory(JsonElement element, string currentPath, ImmutableList<FileMetadata>.Builder fileList)
        {
            if (element.TryGetProperty("files", out JsonElement files))
            {
                foreach (var file in files.EnumerateArray())
                {
                    var filename = file.GetProperty("filename").GetString();
                    var relativePath = file.GetProperty("relative_path").GetString();
                    var sizeMb = file.GetProperty("size_mb").GetDouble();
                    var createdDate = DateTime.Parse(file.GetProperty("created_date").GetString());

                    var fileMetadata = new FileMetadata(filename, relativePath, sizeMb, createdDate);
                    fileList.Add(fileMetadata);
                }
            }

            foreach (var property in element.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    ParseDirectory(property.Value, Path.Combine(currentPath, property.Name), fileList);
                }
            }
        }
    }
}