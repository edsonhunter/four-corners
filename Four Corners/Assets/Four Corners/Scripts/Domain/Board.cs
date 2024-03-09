﻿using Four_Corners.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Four_Corners.Domain
{
    public class Board : IBoard
    {
        public IList<IList<ITile>> Tiles => _tiles;
        private IList<IList<ITile>> _tiles { get; set; }

        private Board()
        {
            _tiles = new List<IList<ITile>>();
        }

        public Board(IList<IList<ITile>> tiles)
        {
            _tiles = new List<IList<ITile>>(tiles);
        }
    }
}