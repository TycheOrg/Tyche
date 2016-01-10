using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyche.Tests.Helpers;
using System.Linq;
using System.Collections.Generic;

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

        [TestMethod]
        public void NoDataDoesNotThrowError()
        {
            var g = new Generator(new TestSource());

            var name = g.Generate();

            Assert.IsTrue(string.IsNullOrWhiteSpace(name), "Should generate empty name if no data exists");
        }

        [TestMethod]
        public void CategoriesWithNoInternalDataDoesNotThrowError()
        {
            var g = new Generator(new TestSource(Data.Morphemes, null, new Dictionary<string, IList<string>> { ["first"] = new List<string>(), ["second"] = new List<string>() }));

            var name = g.Generate();

            Assert.IsTrue(string.IsNullOrWhiteSpace(name), "Should generate empty name if no data exists");
        }

        [TestMethod]
        public void FirstLettersAreTheSame()
        {
            var g = new Generator();
            g.ResetPreviousNames();
            g.SavePreviousNames();
            for (int i = 0; i < 500; i++)
            {
                //var g = new Generator(new TestSource(Data.Morphemes, null, Data.Categories));
                var name = g.Generate();
                Assert.IsTrue(CheckFirstLetters(name), $"{name} does not have matching first letters for each word");
            }
        }

        private bool CheckFirstLetters(string name)
        {
            var firstLetters = name.ToLower().Split(' ').Take(2).Select(w => w[0]).Distinct().Skip(1);
            return !firstLetters.Any();
        }
    }
}
