using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Underscore;

namespace Tyche
{
    public class Generator
    {
        private static int MAX_RECURSION_COUNT = 1024;
        
        private ISource Source { get; }

        public Generator(ISource source = null)
        {
            Source = source ?? new DefaultSource();
        }

        public string Generate(string category = null, bool failOnInvalidCatgeory = true)
        {
            var categories = GetAvailableCategories().ToList();
            if (!categories.Any())
            {
                return "";
            }

            if (!Source.Categories.Any(c => c.Value.Any()) || !Source.Morphemes.Any())
            {
                return "";
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                category = _.List.Shuffle(categories).First();
            }
            else if (!Source.Categories.ContainsKey(category) && failOnInvalidCatgeory)
            {
                throw new ArgumentException($"Invalid Argument Specified: {category}");
            }
            else if (!failOnInvalidCatgeory)
            {
                category = _.List.Shuffle(categories).First();
            }

            var name = GenerateName(category, 0);
            SavePreviousNames();

            return name;
        }

        public void ResetPreviousNames()
        {
            Source.ResetPreviousNames();   
        }

        public void SavePreviousNames()
        {
            Source.SavePreviousNames();
        }

        public IEnumerable<string> GetAvailableCategories() => Source.Categories.Keys;

        private string GenerateName(string category, int recursionCount)
        {
            if (recursionCount >= MAX_RECURSION_COUNT)
            {
                throw new StackOverflowException("No name can be generated. Choosing another category may help");
            }

            Source.Morphemes = _.List.Shuffle(Source.Morphemes);
            var adjective = Source.Morphemes.First();

            // Take only animals that the first letter is the same as the first letter of the adjective
            var filteredCategories = Source.Categories[category].Where(a => a.ToUpper()[0] == adjective.ToUpper()[0]).ToList();

            // If we didn't find any matching animals - try again
            if (!filteredCategories.Any())
            {
                return GenerateName(category, ++recursionCount);
            }

            filteredCategories = _.List.Shuffle(filteredCategories).ToList();

            var name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase($"{adjective} {filteredCategories.First()}");

            // If the name was already given - try again
            if (Source.PreviousNames.Contains(name))
            {
                return GenerateName(category, ++recursionCount);
            }

            UpdatePreviousNames(name);
            return name;
        }

        private void UpdatePreviousNames(string name)
        {
            Source.PreviousNames.Add(name);
            SavePreviousNames();
        }
    }
}
