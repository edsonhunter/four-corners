using Four_Corners.Domain.Interface;
using Four_Corners.Service;

namespace Four_Corners.Domain
{
    public class Spawner : ISpawner
    {
        public ElfColor Color { get; private set; }

        public ITile Tile { get; private set; }

        private Spawner()
        {
            Color = ElfColor.Unknown;
        }

        public Spawner(ElfColor color, ITile tile) : this()
        {
            Color = color;
            Tile = tile;
        }

        public IElf SpawnNewElf()
        {
            return Factory.CreateElf(Color, Tile);
        }
    }
}