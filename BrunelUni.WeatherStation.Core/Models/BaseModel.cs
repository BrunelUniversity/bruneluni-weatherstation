using System;
using System.ComponentModel.DataAnnotations;

namespace BrunelUni.WeatherStation.Core.Models;

public abstract class BaseModel
{
    [ Key ]
    public string Id { set; get; }
    
    public DateTime Created { set; get; } = DateTime.UtcNow;
    
    protected BaseModel( )
    {
        Id = Guid.NewGuid( ).ToString( );
        Created = DateTime.UtcNow;
    }
}