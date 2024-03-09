using Four_Corners.Domain.Interface;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Domain
{
    public class Elf : IElf
    {
        public System.Guid Id { get; }
        public ElfColor Color { get; private set; }

        public bool Alive { get; private set; }

        public ITile CurrentTile { get; private set; }
        private CancellationTokenSource cancellationTokenSource;

        private Elf()
        {
            Id = System.Guid.Empty;
            Color = ElfColor.Unknown;
        }

        public Elf(ElfColor color, ITile currentTile)
        {
            Id = System.Guid.NewGuid();
            Color = color;
            CurrentTile = currentTile;
            Alive = true;
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(LifeCicle, cancellationTokenSource.Token);
            Task.Run(MovementLoop, cancellationTokenSource.Token);
        }

        public bool Move(ITile tile)
        {
            CurrentTile = tile;
            Debug.Log($"Ops! The sit is taken? {tile.Occupied}");
            return !tile.Occupied;
        }

        public async Task MovementLoop()
        {
            while (Alive)
            {
                if (!Alive)
                {
                    break;
                }
                
                await Task.Delay(new System.Random().Next(1000, 5000), cancellationTokenSource.Token);
                Debug.Log("I'll move!");
                var tileToMove = CurrentTile.Neighbors[new System.Random().Next(CurrentTile.Neighbors.Count)];
                tileToMove.MoveToHere(this);
            }
        }

        public async Task LifeCicle()
        {
            while (Alive)
            {
                if (!Alive)
                {
                    break;
                }

                await Task.Delay(1000, cancellationTokenSource.Token);
                Debug.Log($"I'm elf {Color}-{Id}");

                int tossCoin = new System.Random().Next(0, 100);
                if (tossCoin % 2 == 0)
                {
                    Alive = false;
                }

                Debug.Log($"[DEAD] I WAS elf {Color}-{Id}");
            }
        }

        public void Kill()
        {
            Alive = false;
            cancellationTokenSource.Cancel();
        }
    }
}