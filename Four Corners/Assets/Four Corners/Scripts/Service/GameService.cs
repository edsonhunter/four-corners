using Four_Corners.Domain.Interface;
using Four_Corners.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Four_Corners.Service
{
    public class GameService : IGameService
    {
        private IMatch Match { get; set; }
        private IBoard Board { get; set; }

        public GameService()
        {
            
        }

        private void CreateBoard()
        {
            
        }

        public void CreateMatch()
        {
            var spawnerList = new List<ISpawner>();
            for(int idx = 0; idx < Enum.GetValues(typeof(ElfColor)).Length; idx++)
            {
                spawnerList.Add(Factory.CreateSpawner((ElfColor)idx, Board.Tiles[idx]));
            }
            Match = Factory.CreatePartida(spawnerList);
            Match.StartMatch();
        }

        public async Task StartGame()
        {
            while (Match.Elves.Count > 2)
            {
                if (Match.Elves.Count <= 1)
                {
                    break;
                }

                await Task.Delay(new System.Random().Next(1000, 5000));
                Debug.Log("New elf alive");
                Match.SpawnNewElfFromSpawner();
            }
        }


        //Could create an elf from a spawner or from the collision of same color elves
        private void SpawnElf(ElfColor newColor, int originX, int originY)
        {
            Match.SpawnNewElfFromSpawner();
        }

        public static ElfColor ChooseNewColor()
        {
            System.Random random = new System.Random();
            int nextColor = random.Next(0, 4);
            return (ElfColor)nextColor;
        }
    }
}