using System.Collections;
using UnityEngine;

namespace Four_Corners.Domain.Interface
{
    public interface ISpawner
    {
        ElfColor Color { get; }
        ITile Tile { get; }

        IElf SpawnNewElf();
    }
}