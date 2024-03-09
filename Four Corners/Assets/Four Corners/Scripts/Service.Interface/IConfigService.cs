using Four_Corners.Domain.Interface;

namespace Four_Corners.Service.Interface
{
    public interface IConfigService : IService
    {
        IConfig Config { get; }
    }
}