namespace Four_Corners.Domain.Interface
{
    public interface IConfig
    {
        IGameConfig GameConfig { get; }
    }

    public interface IGameConfig
    {
        int Width { get; }
        int Height { get; }
    }
}