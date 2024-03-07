using System.Threading.Tasks;

namespace Four_Corners.Service.Interface
{
    public interface IGameService : IService
    {
        void CreateMatch();
        Task StartGame();
    }
}