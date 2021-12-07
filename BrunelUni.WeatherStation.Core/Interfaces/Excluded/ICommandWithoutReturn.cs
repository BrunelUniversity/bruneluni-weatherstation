using Aidan.Common.Core;

namespace BrunelUni.WeatherStation.Core.Interfaces.Excluded;

public interface ICommandWithoutReturn
{
    Result Run( );
}