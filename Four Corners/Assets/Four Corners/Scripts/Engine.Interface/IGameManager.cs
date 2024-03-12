using Four_Corners.Domain.Interface;
using System;
using System.Threading.Tasks;

namespace Four_Corners.Manager.Interface
{
    public interface IGameManager : IManager
    {
        IGameConfig GameConfig { get; }
        Task<IMatch> PrepareMatch(Action<IElf> onElfSpawn);
        void StartGame();
        void EndGame();
    }
}