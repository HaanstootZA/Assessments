using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;

namespace Assessment.Ozow.Conway.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BoardTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Board board = new Board(Size.Empty);
            Assert.IsNotNull(board);
            Assert.AreEqual(board.Size, Size.Empty);
        }

        [TestMethod]
        public void TestArrayConstructor()
        {
            Board board = new Board(new bool[0, 0]);
            Assert.IsNotNull(board);
            Assert.AreEqual(board.Size, Size.Empty);
        }

        [TestMethod]
        public void TestIndexer()
        {
            Board board = new Board(new Size(10, 10));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = true;
                    Assert.IsTrue(board[i, j]);
                }
            }
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestIndexerOutOfRangeSet()
        {
            Board board = new Board(Size.Empty);
            board[2, 2] = true;
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestIndexerOutOfRangeGet()
        {
            Board board = new Board(Size.Empty);
            Assert.IsFalse(board[2, 2]);
            Assert.Fail();
        }

        [TestMethod]
        public void TestPointIndexer()
        {
            Board board = new Board(new Size(10, 10));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Point testPoint = new Point(i, j);
                    board[testPoint] = true;
                    Assert.IsTrue(board[testPoint]);
                }
            }
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestPointIndexerOutOfRangeSet()
        {
            Board board = new Board(Size.Empty);
            board[new Point(2, 2)] = true;
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestPointIndexerOutOfRangeGet()
        {
            Board board = new Board(Size.Empty);
            Assert.IsFalse(board[new Point(2, 2)]);
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraverse()
        {
            bool[,] testSeed = new bool[,] { { true, false }, { false, true } };
            Board board = new Board(testSeed);
            int totalTraversed = 0;
            board.Traverse((p, b) =>
            {
                Assert.AreEqual(testSeed[p.X, p.Y], b);
                totalTraversed += 1;
            });
            Assert.AreEqual(totalTraversed, testSeed.Length);
        }

        [TestMethod]
        public void TestEquals()
        {
            bool[,] b1TestSeed = new bool[,] { { true, false }, { false, true } };
            bool[,] b2TestSeed = new bool[,] { { true, false }, { false, true } };
            Board b1 = new Board(b1TestSeed);
            Board b2 = new Board(b2TestSeed);
            Assert.IsTrue(b1.Equals(b2));
        }

        [TestMethod]
        public void TestReferenceEquals()
        {
            Board b1 = new Board();
            Assert.IsTrue(b1.Equals(b1));
        }

        [TestMethod]
        public void TestNullEquals()
        {
            Board b1 = new Board();
            Assert.IsFalse(b1.Equals(null));
        }

        [TestMethod]
        public void TestTypeEquals()
        {
            Board b1 = new Board();
            Assert.IsFalse(b1.Equals(20));
        }

        [TestMethod]
        public void TestSizeEquals()
        {
            bool[,] b1TestSeed = new bool[,] { { true, false }, { false, true } };
            bool[,] b2TestSeed = new bool[,] { { true, false }, { false, true }, { true, false } };
            Board b1 = new Board(b1TestSeed);
            Board b2 = new Board(b2TestSeed);
            Assert.IsFalse(b1.Equals(b2));
        }

        [TestMethod]
        public void TestDifferentValuesEquals()
        {
            bool[,] b1TestSeed = new bool[,] { { true, false }, { false, true } };
            bool[,] b2TestSeed = new bool[,] { { true, false }, { true, true } };
            Board b1 = new Board(b1TestSeed);
            Board b2 = new Board(b2TestSeed);
            Assert.IsFalse(b1.Equals(b2));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            bool[,] b1TestSeed = new bool[,] { { true, false }, { false, true } };
            Board b1 = new Board(b1TestSeed);
            Assert.AreEqual(b1TestSeed.GetHashCode(), b1.GetHashCode());
        }
    }
}
