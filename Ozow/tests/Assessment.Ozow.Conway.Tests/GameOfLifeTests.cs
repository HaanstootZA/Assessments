using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Ozow.Conway;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assessment.Ozow.Conway.Tests
{
    [TestClass]
    public class GameOfLifeTests
    {
        public GameOfLife GetDefault()
        {
            return new GameOfLife(new bool[0, 0], 0);
        }

        [TestMethod]
        public void TestConstructor()
        {
            GameOfLife gameInstance = new GameOfLife(new bool[0,0], 0);
            Assert.IsNotNull(gameInstance);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorException()
        {
            _ = new GameOfLife(null, 0);
            Assert.Fail();
        }

        [TestMethod]
        public void TestGetEnumertor()
        {
            GameOfLife gameInstance = this.GetDefault();
            Assert.IsNotNull(gameInstance.GetEnumerator());
        }

        [TestMethod]
        public void TestExplicitGetEnumertor()
        {
            GameOfLife gameInstance = this.GetDefault();
            Assert.IsNotNull(((IEnumerable)gameInstance).GetEnumerator());
        }

        [TestMethod]
        public void TestEnumerator()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 2);
            IEnumerator<bool[,]> enumerator = gameOfLife.GetEnumerator();
            enumerator.MoveNext();
            this.CompareCanvas(Boards.BLINKER_GEN_1, enumerator.Current);

            enumerator.MoveNext();
            this.CompareCanvas(Boards.BLINKER_GEN_0, enumerator.Current);
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestEnumeratorReset()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 0);
            IEnumerator<bool[,]> enumerator = gameOfLife.GetEnumerator();
            enumerator.Reset();
            Assert.Fail();
        }

        [TestMethod]
        public void TestEnumeratorDispose()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 1);
            using (IEnumerator<bool[,]> enumerator = gameOfLife.GetEnumerator())
            {
                enumerator.MoveNext();
            }
            Assert.IsTrue(true);
        }

        private void CompareCanvas(bool[,] expectedMatrix, bool[,] actualMatrix)
        {
            Assert.AreEqual(expectedMatrix.Rank, actualMatrix.Rank);
            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.Length, actualMatrix.Length);

            int width = expectedMatrix.GetLength(0);
            int height = expectedMatrix.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Assert.AreEqual(expectedMatrix[x, y], actualMatrix[x, y]);
                }
            }
        }
    }
}
