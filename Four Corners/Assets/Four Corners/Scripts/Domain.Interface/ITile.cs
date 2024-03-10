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

        delegate void ElfBehaviorDelegate(IElf parent);
        event ElfBehaviorDelegate OnElfSpawn;
        event ElfBehaviorDelegate OnElfDestroy;

        void AddNeighbor(ITile tile);
        void MoveToHere(IElf elf);
        void RemoveThisElf(IElf elf);
    }
}