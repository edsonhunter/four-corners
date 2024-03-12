using Four_Corners.Domain.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Four_Corners.Domain
{
    public class Tile : ITile
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public bool Occupied => _elvesInTheTile.Count > 0;

        public IList<ITile> Neighbors => _neighbors.AsReadOnly();
        private List<ITile> _neighbors { get; set; }

        public IList<IElf> ElvesInTheTile => _elvesInTheTile.AsReadOnly();
        public List<IElf> _elvesInTheTile { get; private set; }

        public event Action<ElfColor, ITile> OnElfSpawn { add => _onElfSpawn += value; remove => _onElfSpawn -= value; }
        private Action<ElfColor, ITile> _onElfSpawn;
        public event Action<IElf> OnElfDestroy { add => _onElfDestroy += value; remove => _onElfDestroy -= value; }
        public Action<IElf> _onElfDestroy;

        private object ElfMovingHere = new object();

        private Tile()
        {
            X = -1;
            Y = -1;
            _elvesInTheTile = new List<IElf>();
            _neighbors = new List<ITile>();
        }

        public Tile(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public void MoveToHere(IElf elf)
        {
            if (elf.Move(this))
            {
                lock (ElfMovingHere)
                {
                    _elvesInTheTile.Add(elf);
                }
                return;
            }

            for(int elfIdx = ElvesInTheTile.Count -1; elfIdx >= 0;)
            {
                var elfInTile = ElvesInTheTile[elfIdx];
                if (elfInTile.Color != elf.Color)
                {
                    elfInTile.Kill();
                    elf.Kill();
                    _onElfDestroy.Invoke(elfInTile);
                    _onElfDestroy.Invoke(elf);

                    RemoveThisElf(elfInTile);
                    break;
                }

                _onElfSpawn.Invoke(elf.Color, elf.CurrentTile);
                break;
            }
        }

        public void AddNeighbor(ITile tile)
        {
            _neighbors.Add(tile);
        }

        public void RemoveThisElf(IElf elf)
        {
            lock (ElfMovingHere)
            {
                _elvesInTheTile.Remove(elf);
            }
        }
    }
}