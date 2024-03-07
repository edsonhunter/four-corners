using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface IMatch
    {
        IList<IElf> Elves { get; }
        IList<ISpawner> Spawners { get; }

        void SpawnNewElfFromSpawner();
        void SpawnNewElf(ElfColor color, int sourceX, int sourceY);
        void StartMatch();
    }
}