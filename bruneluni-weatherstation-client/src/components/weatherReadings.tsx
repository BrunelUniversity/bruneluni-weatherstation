import React from "react";
import HumidityReading from "./humidityReading";
import TemperatureReading from "./temperatureReading";
import {useCurrentWeatherContext} from "../states/currentWeatherState";
import Loader from "react-loader-spinner";
import HumidityGraph from "./humidityGraph";
import TemperatureGraph from "./temperatureGraph";
import {useWeatherReadingsContext} from "../states/weatherReadingsState";

const WeatherReadings = ( ): JSX.Element => {
    const humidityReadings = useWeatherReadingsContext().humidityReadings;
    const temperatureReadings = useWeatherReadingsContext().tempReadings;
    const initial = useWeatherReadingsContext().initial;
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
        <HumidityGraph readings={humidityReadings}/>
        <TemperatureGraph readings={temperatureReadings}/>
    </div>
}

export default WeatherReadings;