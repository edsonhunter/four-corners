using Four_Corners.Domain.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Four_Corners.Service.Interface
{
    public interface IGameService : IService
    {
        IMatch CreateMatch();
        void EndGame();
        Task StartGame(CancellationToken token);
    }
}