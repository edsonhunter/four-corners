using Four_Corners.Domain.Interface;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Domain
{
    public class Elf : IElf
    {
        public System.Guid Id { get; }
        public ElfColor Color { get; private set; }

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public bool Alive { get; private set; }

        private Elf()
        {
            Id = System.Guid.Empty;
            Color = ElfColor.Unknown;
            PosX = -1;
            PosY = -1;
        }

        public Elf(ElfColor color, int posX, int posY)
        {
            Id = System.Guid.NewGuid();
            Color = color;
            PosX = posX;
            PosY = posY;
            Alive = true;

            Task.Run(LifeCicle);
        }

        public void Move(int newX, int newY)
        {
            PosX = newX;
            PosY = newY;
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
    }
}