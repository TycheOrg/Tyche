using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyche
{
    public interface ISource
    {
        IList<string> PreviousNames { get; set; }
        Dictionary<string, IList<string>> Categories { get; set; }
        IList<string> Morphemes { get; set; }

        void ResetPreviousNames();
        void SavePreviousNames();
    }
}
