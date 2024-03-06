using Four_Corners.Domain.Interface;

namespace Four_Corners.Domain
{
    public class Elf : IElf
    {
        public ElfColor Color { get; private set; }

        public int PosX { get; private set; }
        public int PosY { get; private set; }


        private Elf()
        {
            Color = ElfColor.Unknown;
            PosX = -1;
            PosY = -1;
        }

        public Elf(ElfColor color, int posX, int posY)
        {

        }
    }
}