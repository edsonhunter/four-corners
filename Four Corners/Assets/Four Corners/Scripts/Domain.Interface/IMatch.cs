using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface IMatch
    {
        IBoard Board { get; }
        IList<IElf> Elves { get; }
        IList<ISpawner> Spawners { get; }
        bool Running { get; }

        ISpawner ChooseRandomSpawner();
        IElf SpawnNewElfFromSpawner();
        IElf SpawnNewElf(ElfColor color, ITile sourceTile);
        void RemoveElf(IElf elf);
        void StartMatch();
        void EndMatch();
    }
}