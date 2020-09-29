using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Ozow.Conway;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace Assessment.Ozow.Conway.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class GameOfLifeTests
    {
        public GameOfLife GetDefault()
        {
            return new GameOfLife(new Board(Size.Empty), 0);
        }

        [TestMethod]
        public void TestConstructor()
        {
            GameOfLife gameInstance = new GameOfLife(new Board(Size.Empty), 0);
            Assert.IsNotNull(gameInstance);
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
            IEnumerator<Board> enumerator = gameOfLife.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(Boards.BLINKER_GEN_0, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(Boards.BLINKER_GEN_1, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(Boards.BLINKER_GEN_0, enumerator.Current);
        }

        [TestMethod]
        public void TestEnumeratorRepeats()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 0);

            IEnumerator<Board> enumerator = gameOfLife.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod, ExpectedException(typeof(NotImplementedException))]
        public void TestEnumeratorReset()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 0);
            IEnumerator<Board> enumerator = gameOfLife.GetEnumerator();
            enumerator.Reset();
            Assert.Fail();
        }

        [TestMethod]
        public void TestEnumeratorDispose()
        {
            GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 1);
            using (IEnumerator<Board> enumerator = gameOfLife.GetEnumerator())
            {
                enumerator.MoveNext();
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestDispose()
        {
            using (GameOfLife gameOfLife = new GameOfLife(Boards.BLINKER_GEN_0, 1))
            {
                Assert.IsTrue(true);
            }
        }
    }
}
