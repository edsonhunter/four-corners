namespace Four_Corners.Domain.Interface
{
    public interface IElf
    {
        ElfColor Color { get; }
        int PosX { get; }
        int PosY { get; }

        void Move(int newX, int newY);
    }

    public enum ElfColor
    {
        Unknown = -1,
        Red = 0,
        Yellow = 1,
        Blue = 2,
        Black = 3,
    }
}
