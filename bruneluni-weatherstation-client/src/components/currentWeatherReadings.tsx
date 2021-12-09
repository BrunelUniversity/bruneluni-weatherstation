import React from "react";
import HumidityReading from "./humidityReading";
import TemperatureReading from "./temperatureReading";
import {useCurrentHumidityContext} from "../states/currentHumidityState";
import {useCurrentTemperatureContext} from "../states/currentTemperatureState";

const CurrentWeatherReadings = ( ): JSX.Element => {
    const humidity = useCurrentHumidityContext().currentHumidity;
    const temperature = useCurrentTemperatureContext().currentTemperature;
    return <div>
        <HumidityReading readingAt={humidity.readingAt} relativeHumidity={humidity.relativeHumidity}/>
        <TemperatureReading readingAt={temperature.readingAt} celsius={temperature.celsius}/>
    </div>
}

export default CurrentWeatherReadings;