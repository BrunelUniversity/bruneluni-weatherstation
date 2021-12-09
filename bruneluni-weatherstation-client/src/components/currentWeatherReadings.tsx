import React from "react";
import HumidityReading from "./humidityReading";
import TemperatureReading from "./temperatureReading";
import {useCurrentWeatherContext} from "../states/currentWeatherState";
import Loader from "react-loader-spinner";

const CurrentWeatherReadings = ( ): JSX.Element => {
    const humidity = useCurrentWeatherContext().currentHumidity;
    const temperature = useCurrentWeatherContext().currentTemperature;
    const initial = useCurrentWeatherContext().initial;
    if(!initial){
        return <Loader
            type="TailSpin"
            color='#B8B8B8'
            height={75}
            width={75}
            radius={1}
            timeout={15000}
        />
    }
    return <div>
        <HumidityReading readingAt={humidity.readingAt} relativeHumidity={humidity.relativeHumidity}/>
        <TemperatureReading readingAt={temperature.readingAt} celsius={temperature.celsius}/>
    </div>
}

export default CurrentWeatherReadings;