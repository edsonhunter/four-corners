using Four_Corners.Domain.Interface;
using Four_Corners.Service;

namespace Four_Corners.Domain
{
    public class Spawner : ISpawner
    {
        public ElfColor Color { get; private set; }

        public int PosX { get; private set; }

        public int PosY { get; private set; }

        private Spawner()
        {
            Color = ElfColor.Unknown;
            PosX = -1;
            PosY = -1;
        }

        public Spawner(ElfColor color, int originX, int originY) : this()
        {
            Color = color;
            PosX = originX;
            PosY = originY;
        }

        public IElf SpawnNewElf()
        {
            return Factory.CreateElf(Color, PosX, PosY);
        }
    }
}