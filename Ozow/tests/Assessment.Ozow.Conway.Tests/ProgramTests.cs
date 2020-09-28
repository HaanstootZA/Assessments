using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;

namespace Assessment.Ozow.Conway.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ProgramTests
    {
        [TestMethod]
        public void TestCreateBoard()
        {
            Size expectedSize = new Size(1, 1);
            Board actualBoard = Program.CreateBoard(expectedSize);
            Assert.IsNotNull(actualBoard);
            Assert.AreEqual(expectedSize, actualBoard.Size);
        }
    }
}
