using Four_Corners.Domain.Interface;
using Four_Corners.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Domain
{
    public class Spawner : ISpawner
    {
        public ElfColor Color { get; private set; }

        public ITile Tile { get; private set; }
        private bool Running { get; set; }

        public event Action<ElfColor, ITile> OnElfSpawn { add => _onElfSpawn += value; remove => _onElfSpawn -= value; }
        private Action<ElfColor, ITile> _onElfSpawn;
        private Spawner()
        {
            Color = ElfColor.Unknown;
        }

        public Spawner(ElfColor color, ITile tile) : this()
        {
            Color = color;
            Tile = tile;
            Running = true;
        }

        public IElf SpawnElf()
        {
            var babyElf = Factory.CreateElf(Color, Tile);
            _onElfSpawn.Invoke(Color, Tile);
            return babyElf;
        }

        public void StartGame(CancellationToken token)
        {
            Task.Run(GenerateElfLoop, token);
        }

        private async Task GenerateElfLoop()
        {
            while (Running)
            {
                await Task.Delay(5000);

                SpawnElf();
            }
        }

        public void EndGame()
        {
            Running = false;
        }
    }
}