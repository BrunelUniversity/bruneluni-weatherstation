using System;

namespace BrunelUni.WeatherStation.API;

[ AttributeUsage( AttributeTargets.Class | AttributeTargets.Method ) ]
public class SimpleAuthMeta : Attribute
{
}