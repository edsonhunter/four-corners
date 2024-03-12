using Four_Corners.Domain.Interface;
using Four_Corners.Manager;
using Four_Corners.Manager.Interface;
using UnityEngine;

namespace Four_Corners.Scene.Gameplay
{
    public class GameScene : BaseScene
    {
        [field: SerializeField]
        private GameManager GameManager { get; set; }
        [field: SerializeField]
        private GameObject TilePrefab { get; set; }
        [field: SerializeField]
        private ElfController ElfPrefab { get; set; }

        private IMatch GameMatch { get; set; }


        private async void Start()
        {
            var result = await GameManager.PrepareMatch(SpawnNewElf);
            GameMatch = result;

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

        private void SpawnNewElf(IElf elf)
        {
            ElfController elfObj = Instantiate(ElfPrefab, 
                new Vector3(elf.CurrentTile.X, 0, elf.CurrentTile.Y), Quaternion.identity, transform);
            elfObj.Init(elf);
        }
    }
}