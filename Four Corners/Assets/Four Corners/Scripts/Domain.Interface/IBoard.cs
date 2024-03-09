using System.Collections.Generic;

namespace Four_Corners.Domain.Interface
{
    public interface IBoard
    {
        IList<IList<ITile>> Tiles { get; }
    }
}