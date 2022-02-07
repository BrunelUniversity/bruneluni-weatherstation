import React, {useEffect} from "react";
import {getReadingsAndUpdateState} from "../services/weatherApiService";
import {useWeatherReadingsContext} from "./weatherReadingsState";

const WeatherReadingsStateManager = ( ) =>{
    const setWeather = useWeatherReadingsContext().setCurrentWeatherReadings
    const initial = useWeatherReadingsContext().initial;
    useEffect(()=>{
        if(!initial){
            setTimeout(() => {
                getReadingsAndUpdateState(x => {
                    const [temp, hum] = x;
                    setWeather([hum, temp, true]);
                })
            }, 2000)
        }
    })

    return <React.Fragment/>
}

export default WeatherReadingsStateManager;
