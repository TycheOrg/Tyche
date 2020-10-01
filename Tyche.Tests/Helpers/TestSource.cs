using System.Collections.Generic;

namespace Tyche.Tests.Helpers
{
    public class TestSource : ISource
    {
        public Dictionary<string, IList<string>> Categories { get; set; } = new Dictionary<string, IList<string>>();

        public IList<string> Morphemes { get; set; } = new List<string>();

        public  IList<string> PreviousNames { get; set; } = new List<string>();

        public void ResetPreviousNames()
        {
            PreviousNames.Clear();
        }

        public void SavePreviousNames()
        {
        }

        public TestSource(List<string> morphemes = null, List<string> previousNames = null, Dictionary<string, IList<string>> categories = null )
        {
            if (morphemes != null)
            {
                Morphemes = morphemes;
            }

            if (previousNames != null)
            {
                PreviousNames = previousNames;
            }

            if (categories != null)
            {
                Categories = categories;
            }
        }
    }

    public static class Data
    {
        public static List<string> Morphemes = new List<string>
        {
            "Amazing",
            "Wonderful",
            "Catchy"
        };

        public static Dictionary<string, IList<string>> Categories = new Dictionary<string, IList<string>>
        {
            ["Animals"] = new List<string>
            {
                "Anteater",
                "Wombat"
            },
            ["Things"] = new List<string>
            {
                "Counter",
                "Car"
            }
        };
    }
}
