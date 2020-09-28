using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Assessment.Ozow.Sorting.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TestSimpleSorter
    {
        private const string EXPECTED_OUTPUT = "aaabcceeeeeffhiiiiklllnnnnooooppprrrrssttttuuyzz";
        private string randomizedInput;

        [TestInitialize]
        public void InitializeTests()
        {
            this.randomizedInput = this.RandomizeInput(TestSimpleSorter.EXPECTED_OUTPUT);
        }

        [TestMethod]
        public void TestConstructor()
        {
            SimpleSorter arraySorter = new SimpleSorter();
            Assert.IsNotNull(arraySorter);
        }

        private void SortStringAndTest(string expectedOutput, string input)
        {
            string actualOuput = (new SimpleSorter()).SortString(input);
            Assert.AreEqual(expectedOutput, actualOuput);
        }

        [TestMethod]
        public void TestSortString()
        {
            this.SortStringAndTest(TestSimpleSorter.EXPECTED_OUTPUT, TestSimpleSorter.EXPECTED_OUTPUT);
        }

        [TestMethod]
        public void TestSortStringRandomizedInput()
        {
            this.SortStringAndTest(TestSimpleSorter.EXPECTED_OUTPUT, this.randomizedInput);
        }

        [TestMethod]
        public void TestSortStringUpperCaseCharacters()
        {
            this.SortStringAndTest(TestSimpleSorter.EXPECTED_OUTPUT, this.randomizedInput.ToUpper());
        }

        [TestMethod]
        public void TestSortStringIncludingNonAlphabeticCharacters()
        {
            this.SortStringAndTest(TestSimpleSorter.EXPECTED_OUTPUT, this.randomizedInput + "46, + -");
        }

        [TestMethod]
        public void TestSortStringNullString()
        {
            this.SortStringAndTest(string.Empty, null);
        }

        [TestMethod]
        public void TestSortStringWhiteSpaceString()
        {
            this.SortStringAndTest(string.Empty, "    \t");
        }

        private string RandomizeInput(string input)
        {
            StringBuilder inputBuilder = new StringBuilder(input);
            StringBuilder resultBuilder = new StringBuilder();
            while (inputBuilder.Length > 0)
            {
                int random = RandomNumberGenerator.GetInt32(inputBuilder.Length);
                resultBuilder.Append(inputBuilder[random]);
                inputBuilder.Remove(random, 1);
            }
            return resultBuilder.ToString();
        }
    }
}
