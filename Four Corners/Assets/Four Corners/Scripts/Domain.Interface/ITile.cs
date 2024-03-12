using System;
using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface ITile
    {
        int X { get; }
        int Y { get; }
        bool Occupied { get; }
        IList<ITile> Neighbors { get; }
        IList<IElf> ElvesInTheTile { get; }

        event Action<ElfColor, ITile> OnElfSpawn;
        event Action<IElf> OnElfDestroy;

        void AddNeighbor(ITile tile);
        void MoveToHere(IElf elf);
        void RemoveThisElf(IElf elf);
    }
}