using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Excluded;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract
{
    [ Service( Scope = ServiceLifetimeEnum.Singleton ) ]
    public interface IWeatherDataInitializer : IInitialisable
    {
    }
}