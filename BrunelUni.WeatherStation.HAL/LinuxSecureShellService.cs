using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class LinuxSecureShellService : ILinuxSecureShellService
{
    private readonly IProcessor _processor;
    private readonly ILoggerAdapter<ILinuxSecureShellService> _loggerAdapter;

    public LinuxSecureShellService( IProcessor processor,
        ILoggerAdapter<ILinuxSecureShellService> loggerAdapter )
    {
        _processor = processor;
        _loggerAdapter = loggerAdapter;
    }

    public Result Activate( )
    {
        _loggerAdapter.LogInfo( "secure shell activated" );
        return _processor
            .RunAndWait( "sudo service", "ssh start" )
            .Status == OperationResultEnum.Success
            ? Result.Success( )
            : Result.Error( "" );
    }

    public Result Deactivate( )
    {
        _loggerAdapter.LogInfo( "secure shell de-activated" );
        return _processor
            .RunAndWait( "sudo service", "ssh stop" )
            .Status == OperationResultEnum.Success
            ? Result.Success( )
            : Result.Error( "" );
    }
}