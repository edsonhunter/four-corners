using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface IMatch
    {
        IList<IElf> Elves { get; }
        IList<ISpawner> Spawners { get; }
        bool Running { get; }

        void SpawnNewElfFromSpawner();
        void SpawnNewElf(ElfColor color, ITile sourceTile);
        void StartMatch();
        void EndMatch();
    }
}