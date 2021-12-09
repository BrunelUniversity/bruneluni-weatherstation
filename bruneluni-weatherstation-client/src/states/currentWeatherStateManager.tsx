import React, {useEffect, useState} from "react";
import {useCurrentWeatherContext} from "./currentWeatherState";
import {getCurrentAndUpdateState} from "../services/weatherApiService";

const CurrentWeatherStateManager = ( ) =>{
    const setWeather = useCurrentWeatherContext().setCurrentWeather
    const initial = useCurrentWeatherContext().initial;
    useEffect(()=>{
        if(!initial){
            setTimeout(() => {
                getCurrentAndUpdateState(x => {
                    const [temp, hum] = x;
                    setWeather([hum, temp, true]);
                })
            }, 500)
        }
    })

    return <React.Fragment/>
}

export default CurrentWeatherStateManager;