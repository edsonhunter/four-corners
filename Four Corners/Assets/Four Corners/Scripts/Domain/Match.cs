using Four_Corners.Domain.Interface;
using Four_Corners.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Four_Corners.Domain
{
    public class Match : IMatch
    {
        public IBoard Board { get; private set; }
        public IList<IElf> Elves => _elves.AsReadOnly();
        private List<IElf> _elves { get; set; }
        public IList<ISpawner> Spawners => _spawners.AsReadOnly();
        private List<ISpawner> _spawners { get; set; }

        public bool Running { get; private set; }
        private object elfLock = new object();

        private Match()
        {
            _elves = new List<IElf>();
            _spawners = new List<ISpawner>();
        }

        public Match(IBoard board, IList<ISpawner> spawners) : this()
        {
            Board = board;
            _spawners = new List<ISpawner>(spawners);
        }

        public void StartMatch()
        {
            foreach (var spw in Spawners)
            {
                SpawnNewElf(spw.Color, spw.Tile);
            }

            Running = true;
        }

        public void SpawnNewElfFromSpawner()
        {
            var spawner = ChooseRandomSpawner();
            SpawnNewElf(spawner.Color, spawner.Tile);
        }

        private ISpawner ChooseRandomSpawner()
        {
            return Spawners[new Random().Next(0, Spawners.Count)];
        }

        public void SpawnNewElf(ElfColor color, ITile sourceTile)
        {
            lock (elfLock)
            {
                _elves.Add(Factory.CreateElf(color, sourceTile));
            }
        }

        public void RemoveElf(IElf elf)
        {
            lock (elfLock)
            {
                _elves.Remove(elf);
            }
        }

        public void EndMatch()
        {
            Running = false;
            lock (elfLock)
            {
                Parallel.ForEach(Elves, elf => { elf.Kill(); });
                _elves.Clear();
            }
        }
    }
}