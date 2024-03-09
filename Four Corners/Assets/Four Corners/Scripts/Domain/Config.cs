using Four_Corners.Domain.Interface;

namespace Four_Corners.Domain
{
    public class Config : IConfig
    {
        public IGameConfig GameConfig { get; private set; }

        public Config(IGameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }

    public class GameConfig : IGameConfig
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GameConfig(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}