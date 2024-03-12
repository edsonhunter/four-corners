using System;
using System.Threading;
using System.Threading.Tasks;

namespace Four_Corners.Domain.Interface
{
    public interface ISpawner
    {
        ElfColor Color { get; }
        ITile Tile { get; }
        event Action<ElfColor, ITile> OnElfSpawn;

        void StartGame(CancellationToken token);
        void EndGame();
        IElf SpawnElf();
    }
}