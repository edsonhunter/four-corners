using Four_Corners.Domain.Interface;
using Four_Corners.Manager;
using Four_Corners.Manager.Interface;
using Four_Corners.Service;
using Four_Corners.Service.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Scene.Gameplay
{
    public class GameScene : BaseScene
    {
        [field: SerializeField]
        private GameObject TilePrefab { get; set; }
        [field: SerializeField]
        private GameObject ElfPrefab { get; set; }

        private IGameManager GameManager { get; set; }
        private IMatch GameMatch { get; set; }

        private void Awake()
        {
            GameManager = new GameManager();
            GameMatch = GameManager.PrepareMatch();
        }

        private void Start()
        {
            for (int i = 0; i < GameManager.GameConfig.Width; i++)
            {
                for (int j = 0; j < GameManager.GameConfig.Height; j++)
                {
                    var tileObject = Instantiate(TilePrefab, this.transform);
                    var tile = GameMatch.Board.Tiles[i][j];
                    tileObject.transform.position = new Vector2(tile.X, tile.Y);
                    tileObject.name = $"{i + 1}:{j + 1}";
                }
            }

            GameManager.StartGame();
        }

        private void OnDestroy()
        {
            GameManager.EndGame();
        }
    }
}