using Four_Corners.Domain;
using Four_Corners.Domain.Interface;
using System.Collections.Generic;

namespace Four_Corners.Service
{
    public static class Factory
    {
        public static IMatch CreatePartida(IList<ISpawner> spawners)
        {
            return new Match(spawners);
        }

        public static IElf CreateElf(ElfColor color, ITile originTile)
        {
            return new Elf(color, originTile);
        }

        public static ISpawner CreateSpawner(ElfColor color, ITile tile)
        {
            return new Spawner(color, tile);
        }

        public static IBoard CreateBoard(IList<IList<ITile>> board)
        {
            return new Board(board);
        }

        public static IConfig CreateConfig(IGameConfig gameConfig)
        {
            return new Config(gameConfig);
        }

        public static IGameConfig CreateGameConfig(int width, int height)
        {
            return new GameConfig(width, height);
        }
    }
}