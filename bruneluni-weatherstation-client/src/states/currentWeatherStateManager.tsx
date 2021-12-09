import React, {useEffect, useState} from "react";
import {useCurrentTemperatureContext} from "./currentTemperatureState";
import {useCurrentHumidityContext} from "./currentHumidityState";
import {getCurrentHumidity, getCurrentTemperature} from "../services/weatherApiService";

const CurrentWeatherStateManager = ( ) =>{
    const setTemp = useCurrentTemperatureContext().setCurrentTemperature
    const setHumidity = useCurrentHumidityContext().setCurrentHumidity
    const [initial, setInitial] = useState<boolean>(false);
    useEffect(()=>{
        if(!initial){
            getCurrentTemperature(x => setTemp(x))
            getCurrentHumidity(x => setHumidity(x))
            setInitial(true);
        }
    })

    return <React.Fragment/>
}

export default CurrentWeatherStateManager;