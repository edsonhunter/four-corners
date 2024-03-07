using System.Collections;
using UnityEngine;

namespace Four_Corners.Domain.Interface
{
    public interface ISpawner
    {
        ElfColor Color { get; }
        int PosX { get; }
        int PosY { get; }

        IElf SpawnNewElf();
    }
}