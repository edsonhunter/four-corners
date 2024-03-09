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

        delegate void ElfSpawnDelegate(IElf parent);
        event ElfSpawnDelegate OnElfSpawn;

        void AddNeighbor(ITile tile);
        void MoveToHere(IElf elf);
    }
}