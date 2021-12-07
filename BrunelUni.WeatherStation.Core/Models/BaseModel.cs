using System;
using System.ComponentModel.DataAnnotations;

namespace BrunelUni.WeatherStation.Core.Models;

public abstract class BaseModel
{
    [ Key ]
    public string Id { get; } = Guid.NewGuid( ).ToString( );
    
    public DateTime Created { get; } = DateTime.UtcNow;
}