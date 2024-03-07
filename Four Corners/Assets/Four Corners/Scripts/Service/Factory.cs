using Four_Corners.Domain;
using Four_Corners.Domain.Interface;
using System.Collections.Generic;

namespace Four_Corners.Service
{
    public static class Factory
    {
        public static IMatch CreatePartida(IList<ISpawner> spawners)
        {
            return new Match(spawners);
        }

        public static IElf CreateElf(ElfColor color, int startX, int startY)
        {
            return new Elf(color, startX, startY);
        }

        public static ISpawner CreateSpawner(ElfColor color, int startX, int startY)
        {
            return new Spawner(color, startX, startY);
        }
    }
}