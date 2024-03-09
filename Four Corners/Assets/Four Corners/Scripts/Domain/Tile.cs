using Four_Corners.Domain.Interface;
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

        public event ITile.ElfSpawnDelegate OnElfSpawn { add => _onElfSpawn += value; remove => _onElfSpawn -= value; }
        private event ITile.ElfSpawnDelegate _onElfSpawn;

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
                _elvesInTheTile.Add(elf);
                return;
            }

            for(int elfIdx = ElvesInTheTile.Count -1; elfIdx >= 0; elfIdx--)
            {
                var elfInTile = ElvesInTheTile[elfIdx];
                if (elfInTile.Color != elf.Color)
                {
                    Debug.Log("Time to DIE!");
                    elfInTile.Kill();
                    elf.Kill();
                    _elvesInTheTile.Remove(elfInTile);
                    break;
                }

                Debug.Log("Time to procriate");
                _onElfSpawn.Invoke(elf);
                break;
            }
        }

        public void AddNeighbor(ITile tile)
        {
            _neighbors.Add(tile);
        }
    }
}