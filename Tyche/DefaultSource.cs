using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyche
{
    public class DefaultSource : ISource
    {
        private string libPath;
        private string categoryDirectoryPath;
        private string morphemesPath;
        private string previousNamesFilePath;
        private Dictionary<string, string> categoryFilesPaths = new Dictionary<string, string>();

        public Dictionary<string, IList<string>> Categories { get; set; } = new Dictionary<string, IList<string>>();

        public IList<string> Morphemes { get; set; } = new List<string>();

        public IList<string> PreviousNames { get; set; } = new List<string>();

        public DefaultSource()
        {
            previousNamesFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".release-name-generator", "previousNames.json");
            libPath = Path.Combine(Directory.GetCurrentDirectory(), "Lib");
            morphemesPath = Path.Combine(libPath, "adjectives.json");
            categoryDirectoryPath = Path.Combine(libPath, "Categories");
            categoryFilesPaths = new Dictionary<string, string>
            {
                ["Birds"] = Path.Combine(categoryDirectoryPath, "birds.json"),
                ["Fish"] = Path.Combine(categoryDirectoryPath, "fish.json"),
                ["Lizards"] = Path.Combine(categoryDirectoryPath, "lizards.json"),
                ["Reptiles"] = Path.Combine(categoryDirectoryPath, "reptiles.json"),
                ["Mammals"] = Path.Combine(categoryDirectoryPath, "mammals.json")
            };

            Init();
            Load();
        }

        public void ResetPreviousNames()
        {
            PreviousNames.Clear();
            File.WriteAllText(previousNamesFilePath, "");
        }

        public void SavePreviousNames()
        {
            File.WriteAllText(previousNamesFilePath, JsonConvert.SerializeObject(PreviousNames));
        }

        private void Load()
        {
            PreviousNames = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(previousNamesFilePath)) ?? new List<string>();
            Morphemes = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(morphemesPath)) ?? new List<string>();

            foreach (var categoryFile in categoryFilesPaths)
            {
                Categories.Add(categoryFile.Key.ToString(), JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(categoryFile.Value)) ?? new List<string>());
            }
        }

        private void Init()
        {
            if (!File.Exists(previousNamesFilePath))
            {
                FileInfo f = new FileInfo(previousNamesFilePath);
                var d = Directory.CreateDirectory(f.DirectoryName);
                d.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
#pragma warning disable CS0642 // Possible mistaken empty statement
                using (File.Create(previousNamesFilePath));
#pragma warning restore CS0642 // Possible mistaken empty statement
            }

            var filesToCreate = new List<string>
            {
                morphemesPath,
            };

            filesToCreate.AddRange(categoryFilesPaths.Select(kev => kev.Value));

            foreach (var file in filesToCreate)
            {
                if (!File.Exists(file))
                {
                    FileInfo f = new FileInfo(file);
                    Directory.CreateDirectory(f.DirectoryName);
#pragma warning disable CS0642 // Possible mistaken empty statement
                    using (File.Create(file)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                }
            }
        }
    }
}
