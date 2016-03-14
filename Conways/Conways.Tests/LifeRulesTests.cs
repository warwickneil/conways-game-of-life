using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conways.Tests
{
    using Conways.GameOfLife;
    using NUnit.Framework;

    /// <summary>
    /// Any live cell with fewer than two live neighbours dies.
    /// Any live cell with two or three live neighbours lives.
    /// Any live cell with more than three live neighbours dies.
    /// Any live cell with exactly three live neighbours becomes a live cell.
    /// </summary>
    [TestFixture]
    public class LifeRulesTests
    {
        [Test]
        public void LiveCell_FewerThan2Lives_Dies([Values(0, 1)] int liveNeighbours)
        {
            var currentState = CellState.Alive;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void LiveCell_2Or3LiveNeighbours_Lives([Values(2, 3)] int liveNeighbours)
        {
            var currentState = CellState.Alive;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Alive, newState);
        }

        [Test]
        public void LiveCell_MoreThan3LiveNeighbours_Dies([Range(4, 8)] int liveNeighbours)
        {
            var currentState = CellState.Alive;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void DeadCell_Exactly3LiveNeighbours_Lives()
        {
            var liveNeighbours = 3;
            var currentState = CellState.Dead;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Alive, newState);
        }

        [Test]
        public void DeadCell_Fewer3LiveNeighbours_StaysDead([Range(0, 2)] int liveNeighbours)
        {
            var currentState = CellState.Dead;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void DeadCell_MoreThan3LiveNeighbours_StaysDead([Range(4, 8)] int liveNeighbours)
        {
            var currentState = CellState.Dead;
            var newState = LifeRules.GetNewState(currentState, liveNeighbours);

            Assert.AreEqual(CellState.Dead, newState);
        }

        [Test]
        public void CurrentState_When2_ThrowArgumentException()
        {
            var currentState = (CellState)2;
            var liveNeighbours = 0;

            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(), 
                          () => LifeRules.GetNewState(currentState, liveNeighbours));
        }

        [Test]
        public void LiveNeighbours_MoreThan8_ThrowArgumentException()
        {
            var currentState = CellState.Alive;
            var liveNeighbours = 9;
            var paramName = "liveNeighbours";

            Assert.Throws(
                Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName")
                .EqualTo(paramName),
                () => LifeRules.GetNewState(currentState, liveNeighbours));
        }

        [Test]
        public void LiveNeighbours_LessThan0_ThrowArgumentException()
        {
            var currentState = CellState.Alive;
            var liveNeighbours = -1;
            var paramName = "liveNeighbours";

            Assert.Throws(
                Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName")
                .EqualTo(paramName),
                () => LifeRules.GetNewState(currentState, liveNeighbours));
        }
    }
}
