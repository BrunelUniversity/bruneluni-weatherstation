using System.Collections.Generic;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL;

public class GetTemperatureReadingsCommand : IGetTemperatureReadingsCommand
{
    private readonly Context _dbContext;
    private readonly IDHT20Service _dht20Service;

    public GetTemperatureReadingsCommand( Context dbContext, IDHT20Service dht20Service )
    {
        _dbContext = dbContext;
        _dht20Service = dht20Service;
    }
    
    public ObjectResult<IEnumerable<Temperature>> Run( )
    {
        return new ObjectResult<IEnumerable<Temperature>>
        {
            Value = _dbContext.TemperatureReadings,
            Status = OperationResultEnum.Success
        };
    }
}