using Four_Corners.Service;
using Four_Corners.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Four_Corners.Scene.Gameplay
{
    public class GameScene : BaseScene
    {
        IGameService GameService { get; set; }

        private CancellationTokenSource cancellationTokenSource { get; set; }

        private void Awake()
        {
            IConfigService configService = new ConfigService(20,10);
            GameService = new GameService(configService.Config.GameConfig);
            GameService.CreateMatch();

            cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            Task.Run(async () =>
            {
                await GameService.StartGame(cancellationTokenSource.Token);
            }, cancellationTokenSource.Token);
        }

        private void OnDestroy()
        {
            cancellationTokenSource.Cancel();
            GameService.EndGame();
        }
    }
}