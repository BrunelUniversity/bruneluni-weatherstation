using Aidan.Common.Core;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;

namespace BrunelUni.WeatherStation.DAL;

public class CreateTemperatureReadingsCommand : ICreateTemperatureReadingCommand
{
    private readonly Context _dbContext;

    public CreateTemperatureReadingsCommand( Context dbContext )
    {
        _dbContext = dbContext;
    }
    
    public Result Run( )
    {
        _dbContext.TemperatureReadings.Add( new Temperature
        {
            Celsius = 10.10
        } );
        _dbContext.SaveChanges( );
        return Result.Success( );
    }
}