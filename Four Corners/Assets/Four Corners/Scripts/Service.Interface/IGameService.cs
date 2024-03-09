using System.Threading;
using System.Threading.Tasks;

namespace Four_Corners.Service.Interface
{
    public interface IGameService : IService
    {
        void CreateMatch();
        void EndGame();
        Task StartGame(CancellationToken token);
    }
}