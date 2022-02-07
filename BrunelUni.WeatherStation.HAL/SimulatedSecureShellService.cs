using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class SimulatedSecureShellService : ISimulatedSecureShellService
{
    private readonly ILoggerAdapter<ISimulatedSecureShellService> _loggerAdapter;

    public SimulatedSecureShellService( ILoggerAdapter<ISimulatedSecureShellService> loggerAdapter )
    {
        _loggerAdapter = loggerAdapter;
    }

    public Result Activate( )
    {
        _loggerAdapter.LogInfo( "ssh activated" );
        return Result.Success( );
    }

    public Result Deactivate( )
    {
        _loggerAdapter.LogInfo( "ssh de-activated" );
        return Result.Success( );
    }
}