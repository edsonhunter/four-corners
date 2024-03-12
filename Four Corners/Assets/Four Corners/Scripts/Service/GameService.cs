using Four_Corners.Domain;
using Four_Corners.Domain.Interface;
using Four_Corners.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Transforms;
using UnityEngine;

namespace Four_Corners.Service
{
    public class GameService : IGameService
    {
        private IBoard Board { get; set; }
        private IMatch Match { get; set; }
        
        private IGameConfig Config { get; set; }
        private object SpawnLock = new object();
        private Action<IElf> NewElfSpawning { get; set; }

        public GameService(IGameConfig config, Action<IElf> onElfSpawn)
        {
            Config = config;
            NewElfSpawning = onElfSpawn;
        }

        private void CreateBoard()
        {
            IList<IList<ITile>> board = new List<IList<ITile>>();
            
            for (int i = 0; i < Config.Width; i++)
            {
                IList<ITile> line = new List<ITile>();
                for (int j = 0; j < Config.Height; j++)
                {
                    var tile = new Tile(i, j);
                    tile.OnElfSpawn += SpawnElf;
                    tile.OnElfDestroy += KillElf;
                    line.Add(tile);
                }
                board.Add(line);
            }

            Board = Factory.CreateBoard(board);

            AddNeighbors();
        }

        private void AddNeighbors()
        {
            var board = Board.Tiles;

            for (int i = 0; i < Config.Width; i++)
            {
                for (int j = 0; j < Config.Height; j++)
                {
                    ITile tile = board[i][j];

                    if (tile.Y > 0)
                    {
                        tile.AddNeighbor(board[tile.X][tile.Y - 1]);
                    }

                    if (tile.Y < Config.Height - 1)
                    {
                        tile.AddNeighbor(board[tile.X][tile.Y + 1]);
                    }

                    if (tile.X > 0)
                    {
                        tile.AddNeighbor(board[tile.X - 1][tile.Y]);
                    }

                    if (tile.X < Config.Width - 1)
                    {
                        tile.AddNeighbor(board[tile.X + 1][tile.Y]);
                    }
                }
            }            
        }

        public Task<IMatch> CreateMatch()
        {
            TaskCompletionSource<IMatch> tcs = new TaskCompletionSource<IMatch>();

            CreateBoard();

            var spawnerList = new List<ISpawner>();
            for (int idx = 0; idx < Enum.GetValues(typeof(ElfColor)).Length - 1; idx++)
            {
                var random = new System.Random();
                var randomTile = Board.Tiles
                    [random.Next(idx, Config.Width)]
                    [random.Next(idx, Config.Height)];
                var spawner = Factory.CreateSpawner((ElfColor)idx, randomTile);
                spawner.OnElfSpawn += SpawnElf;
                spawnerList.Add(spawner);
            }
            Match = Factory.CreatePartida(Board, spawnerList);
           
            foreach(var spawner in spawnerList)
            {
                spawner.SpawnElf();
            }
            tcs.SetResult(Match);

            return tcs.Task;
        }

        public void StartGame(CancellationToken token)
        {
            Match.StartMatch();

            foreach (var spawner in Match.Spawners)
            {
                spawner.StartGame(token);
            }
        }

        private void SpawnElf(ElfColor color, ITile originTile)
        {
            lock (SpawnLock)
            {
                var babyElf = Match.SpawnNewElf(color, originTile);
                NewElfSpawning(babyElf);
            }
        }

        private void KillElf(IElf deadElf)
        {
            lock(SpawnLock)
            {
                Match.RemoveElf(deadElf);
            }
        }

        public static ElfColor ChooseNewColor()
        {
            System.Random random = new System.Random();
            int nextColor = random.Next(0, 4);
            return (ElfColor)nextColor;
        }

        public void EndGame()
        {
            Match.EndMatch();
        }
    }
}