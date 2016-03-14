using System;

namespace Conways.GameOfLife
{
    public enum CellState
    {
        Alive,
        Dead
    }

    public class LifeRules
    {
        public static CellState GetNewState(CellState currentState, int liveNeighbours)
        {
            if(!Enum.IsDefined(typeof(CellState), currentState))
            {
                throw new ArgumentOutOfRangeException(nameof(currentState));
            }

            if(liveNeighbours > 8 || liveNeighbours < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(liveNeighbours));
            }

            switch (currentState)
            {
                case CellState.Alive:
                    if (liveNeighbours < 2 || liveNeighbours > 3)
                    {
                        return CellState.Dead;
                    }
                    break;
                case CellState.Dead:
                    if (liveNeighbours == 3)
                    {
                        return CellState.Alive;
                    }
                    break;
                //default:
                //    throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
            }

            return currentState;
        }
    }
}
