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
            Running = true;
        }

        public IElf SpawnNewElfFromSpawner()
        {
            var spawner = ChooseRandomSpawner();
            return SpawnNewElf(spawner.Color, spawner.Tile);
        }

        public ISpawner ChooseRandomSpawner()
        {
            return Spawners[new Random().Next(0, Spawners.Count)];
        }

        public IElf SpawnNewElf(ElfColor color, ITile sourceTile)
        {
            lock (elfLock)
            {
                var babyElf = Factory.CreateElf(color, sourceTile);
                _elves.Add(babyElf);
                return babyElf;
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

            Parallel.ForEach(Spawners, spw => { spw.EndGame(); });
        }
    }
}