namespace Four_Corners.Domain.Interface
{
    public interface IElf
    {
        ElfColor Color { get; }
        ITile CurrentTile { get; }
        bool Alive { get; }

        delegate void ElfStatusUpdate();
        event ElfStatusUpdate OnElfStatusUdate;

        bool Move(ITile tile);
        void Kill();
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
