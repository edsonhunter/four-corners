using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface ITile
    {
        int X { get; }
        int Y { get; }
        bool Occupied { get; }
        IList<IElf> ElvesInTheTile { get; }
    }
}