using Four_Corners.Domain.Interface;
using Four_Corners.Service.Interface;

namespace Four_Corners.Service
{
    public class ConfigService : IConfigService
    {
        public IConfig Config { get; private set; }

        public ConfigService(int witdh, int height) 
        {
            Config = Factory.CreateConfig(Factory.CreateGameConfig(witdh, height));
        }
    }
}