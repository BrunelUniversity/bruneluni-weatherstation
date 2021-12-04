using System;

namespace BrunelUni.WeatherStation.API;

[ AttributeUsage( AttributeTargets.Class ) ]
public class LibWrapperAttribute : Attribute
{
    public string Name { get; set; }
}