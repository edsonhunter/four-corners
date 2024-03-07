using Four_Corners.Domain.Interface;
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

            Task.Run(LifeCicle);
        }

        public bool Move(ITile tile)
        {
            CurrentTile = tile;
            Debug.Log($"Ops! The sit is taken? {tile.Occupied}");
            return !tile.Occupied;
        }

        public async Task LifeCicle()
        {
            while (Alive)
            {
                if (!Alive)
                {
                    break;
                }

                await Task.Delay(1000);
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
        }
    }
}