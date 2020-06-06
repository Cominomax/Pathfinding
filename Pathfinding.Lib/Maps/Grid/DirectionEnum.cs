using System;

namespace Pathfinding.Lib.Maps.Grid
{
    /// <summary>
    /// Represent the different directions that a node can take in a GRID.
    /// </summary>
    [Flags]
    public enum DirectionEnum : byte
    {
        Origin  = 0b_0000_0000,
        Up      = 0b_0000_0001,
        Right   = 0b_0000_0010,
        Down    = 0b_0000_0100,
        Left    = 0b_0000_1000,
    }
}
