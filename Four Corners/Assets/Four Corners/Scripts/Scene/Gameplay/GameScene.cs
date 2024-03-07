using Four_Corners.Service;
using Four_Corners.Service.Interface;

namespace Four_Corners.Scene.Gameplay
{
    public class GameScene : BaseScene
    {
        IGameService GameService { get; set; }
        private void Awake()
        {
            GameService = new GameService();
            GameService.CreateMatch();
        }

        private void Start()
        {
            GameService.StartGame();
        }
    }
}