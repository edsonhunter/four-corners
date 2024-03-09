using Four_Corners.Service;
using Four_Corners.Service.Interface;

namespace Four_Corners.Scene.Gameplay
{
    public class GameScene : BaseScene
    {
        IGameService GameService { get; set; }
        private void Awake()
        {
            IConfigService configService = new ConfigService(20,10);
            GameService = new GameService(configService.Config.GameConfig);
            GameService.CreateMatch();
        }

        private void Start()
        {
            GameService.StartGame();
        }

        private void OnDestroy()
        {
            GameService.EndGame();
        }
    }
}