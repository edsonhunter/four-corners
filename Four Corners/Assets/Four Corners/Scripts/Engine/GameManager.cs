using Four_Corners.Domain.Interface;
using Four_Corners.Manager.Interface;
using Four_Corners.Service;
using Four_Corners.Service.Interface;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Manager
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        private IGameService GameService { get; set; }
        private IConfigService ConfigService { get; set; }
        public IGameConfig GameConfig { get; private set; }
        

        private CancellationTokenSource cancellationTokenSource { get; set; }

        public GameManager() {

        }

        public IMatch PrepareMatch()
        {
            cancellationTokenSource = new CancellationTokenSource();

            ConfigService = new ConfigService(10, 5);
            GameConfig = ConfigService.Config.GameConfig;
            GameService = new GameService(GameConfig);
            return GameService.CreateMatch();
        }

        public void StartGame()
        {
            Task.Run(async () =>
            {
                await GameService.StartGame(cancellationTokenSource.Token);
            }, cancellationTokenSource.Token);
        }

        public void EndGame()
        {
            cancellationTokenSource.Cancel();
            GameService.EndGame();
        }
    }
}