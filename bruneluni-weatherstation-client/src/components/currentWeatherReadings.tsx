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
            color='#707070'
            height={75}
            width={75}
            timeout={3000}
        />
    }
    return <div>
        <HumidityReading readingAt={humidity.readingAt} relativeHumidity={humidity.relativeHumidity}/>
        <TemperatureReading readingAt={temperature.readingAt} celsius={temperature.celsius}/>
    </div>
}

export default CurrentWeatherReadings;