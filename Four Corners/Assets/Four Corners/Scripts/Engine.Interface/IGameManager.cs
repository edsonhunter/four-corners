using Four_Corners.Domain.Interface;

namespace Four_Corners.Manager.Interface
{
    public interface IGameManager : IManager
    {
        IGameConfig GameConfig { get; }
        IMatch PrepareMatch();
        void StartGame();
        void EndGame();
    }
}