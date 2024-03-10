using Four_Corners.Domain;
using Four_Corners.Domain.Interface;
using Four_Corners.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Service
{
    public class GameService : IGameService
    {
        private IBoard Board { get; set; }
        private IMatch Match { get; set; }
        
        private IGameConfig Config { get; set; }
        private object SpawnLock = new object();


        public GameService(IGameConfig config)
        {
            Config = config;
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

        public IMatch CreateMatch()
        {
            CreateBoard();

            var spawnerList = new List<ISpawner>();
            for (int idx = 0; idx < Enum.GetValues(typeof(ElfColor)).Length; idx++)
            {
                var random = new System.Random();
                var randomTile = Board.Tiles
                    [random.Next(idx, Config.Width)]
                    [random.Next(idx, Config.Height)];
                spawnerList.Add(Factory.CreateSpawner((ElfColor)idx, randomTile));
            }
            Match = Factory.CreatePartida(Board, spawnerList);
            Match.StartMatch();
            return Match;
        }

        public async Task StartGame(CancellationToken token)
        {
            while (Match.Running)
            {
                if (Match.Elves.Count <= 1)
                {
                    break;
                }

                if (!Match.Running)
                {
                    break;
                }

                await Task.Delay(new System.Random().Next(1000, 5000), token);

                if (!Match.Running)
                {
                    break;
                }

                Debug.Log("New elf alive");
                Match.SpawnNewElfFromSpawner();
            }
        }

        private void SpawnElf(IElf parent)
        {
            lock (SpawnLock)
            {
                Match.SpawnNewElf(parent.Color, parent.CurrentTile);
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