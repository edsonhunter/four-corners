using Four_Corners.Domain.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Four_Corners.Service.Interface
{
    public interface IGameService : IService
    {
        Task<IMatch> CreateMatch();
        void StartGame(CancellationToken token);
        void EndGame();
    }
}