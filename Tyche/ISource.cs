﻿using System.Collections.Generic;

namespace Tyche
{
    public interface ISource
    {
        /// <summary>
        /// The names previously generated by Tyche.
        /// </summary>
        IList<string> PreviousNames { get; set; }
        /// <summary>
        /// Words available to use in generation. Divided by category with the category name as the key.
        /// </summary>
        Dictionary<string, IList<string>> Categories { get; set; }
        /// <summary>
        /// Morphemes to use in generation
        /// </summary>
        IList<string> Morphemes { get; set; }

        /// <summary>
        /// Resets the list of previously generated names.
        /// </summary>
        void ResetPreviousNames();
        /// <summary>
        /// Persists the current state of previously generated names to the underlying data source.
        /// </summary>
        void SavePreviousNames();
    }
}
