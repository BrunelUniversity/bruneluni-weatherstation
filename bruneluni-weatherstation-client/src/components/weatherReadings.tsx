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
        <h1>Humidity Graph</h1>
        <HumidityGraph readings={humidityReadings}/>
        <h1>Temperature Graph</h1>
        <TemperatureGraph readings={temperatureReadings}/>
    </div>
}

export default WeatherReadings;