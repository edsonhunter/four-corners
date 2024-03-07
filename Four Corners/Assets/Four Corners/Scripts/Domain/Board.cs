using Four_Corners.Domain.Interface;
using System.Collections.Generic;

namespace Four_Corners.Domain
{
    public class Board : IBoard
    {
        public IList<ITile> Tiles => _tiles.AsReadOnly();
        private List<ITile> _tiles { get; set; }

        private Board()
        {
            _tiles = new List<ITile>();
        }

        public Board(IList<ITile> tiles)
        {
            _tiles = new List<ITile>(tiles);
        }
    }
}