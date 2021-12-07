using Aidan.Common.Core;

namespace BrunelUni.WeatherStation.Core.Interfaces.Excluded;

public interface ICommandWithReturn<T>
{
    ObjectResult<T> Run( );
}