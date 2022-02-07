using Aidan.Common.Core;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface ISecureShellService
{
    Result Activate( );
    Result Deactivate( );
}