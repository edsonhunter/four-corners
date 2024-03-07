using Four_Corners.Domain.Interface;
using Four_Corners.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Four_Corners.Service
{
    public class GameService : IGameService
    {
        private IMatch Partida { get; set; }

        public GameService()
        {
            
        }

        public void CreateMatch()
        {
            var spawnerList = new List<ISpawner>();
            foreach (ElfColor color in Enum.GetValues(typeof(ElfColor)))
            {
                spawnerList.Add(Factory.CreateSpawner(color, (int)color, (int)color));
            }
            Partida = Factory.CreatePartida(spawnerList);
            Partida.StartMatch();
        }

        public async Task StartGame()
        {
            while (Partida.Elves.Count > 2)
            {
                if (Partida.Elves.Count <= 1)
                {
                    break;
                }

                await Task.Delay(new Random().Next(1000, 5000));

                Partida.SpawnNewElfFromSpawner();
            }
        }


        //Could create an elf from a spawner or from the collision of same color elves
        private void SpawnElf(ElfColor newColor, int originX, int originY)
        {
            Partida.SpawnNewElfFromSpawner();
        }

        public static ElfColor ChooseNewColor()
        {
            System.Random random = new System.Random();
            int nextColor = random.Next(0, 4);
            return (ElfColor)nextColor;
        }
    }
}