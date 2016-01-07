using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyche.Tests.Helpers;
using System.Linq;

namespace Tyche.Tests
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCategorySelectedThrowsException()
        {
            var g = new Generator(new TestSource(Data.Morphemes, null, Data.Categories));

            g.Generate($"{Data.Categories.Keys.First()}-test", true);
        }

        [TestMethod]
        public void InvalidCategorySelectedNoFailFlagPassedInPicksRandomCategory()
        {
            var g = new Generator(new TestSource(Data.Morphemes, null, Data.Categories));
            
            g.Generate($"{Data.Categories.Keys.First()}-test", false);
        }
    }
}
