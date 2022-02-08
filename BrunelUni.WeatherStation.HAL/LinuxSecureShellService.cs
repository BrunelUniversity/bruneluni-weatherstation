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
        var result = _processor
            .RunAndWait( "sudo systemctl ssh start", "" );
        return result.Status == OperationResultEnum.Success
            ? Result.Success( )
            : Result.Error( result.Msg );
    }

    public Result Deactivate( )
    {
        _loggerAdapter.LogInfo( "secure shell de-activated" );
        var result = _processor
            .RunAndWait( "sudo systemctl ssh stop", "" );
        return result.Status == OperationResultEnum.Success
            ? Result.Success( )
            : Result.Error( result.Msg );
    }
}