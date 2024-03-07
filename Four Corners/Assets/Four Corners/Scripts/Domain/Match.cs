using Four_Corners.Domain.Interface;
using Four_Corners.Service;
using System;
using System.Collections.Generic;

namespace Four_Corners.Domain
{
    public class Match : IMatch
    {
        public IList<IElf> Elves => _elves.AsReadOnly();
        private List<IElf> _elves { get; set; }
        public IList<ISpawner> Spawners => _spawners.AsReadOnly();
        private List<ISpawner> _spawners { get; set; }

        private Match()
        {
            _elves = new List<IElf>();
            _spawners = new List<ISpawner>();
        }

        public Match(IList<ISpawner> spawners) : this()
        {
            _spawners = new List<ISpawner>(spawners);
        }

        public void StartMatch()
        {
            foreach(var spw in Spawners)
            {
                SpawnNewElf(spw.Color, spw.Tile);
            }
        }

        public void SpawnNewElfFromSpawner()
        {
            _elves.Add(ChooseRandomSpawner().SpawnNewElf());
        }

        private ISpawner ChooseRandomSpawner()
        {
            return Spawners[new Random().Next(0,Spawners.Count)];
        }

        public void SpawnNewElf(ElfColor color, ITile sourceTile)
        {
            _elves.Add(Factory.CreateElf(color, sourceTile));
        }
    }
}