import React, {createContext, useContext, useState} from "react";
import { Humidity } from "../components/humidityReading";
import {Temperature} from "../components/temperatureReading";

interface CurrentWeatherContext{
    initial: boolean,
    currentHumidity: Humidity,
    currentTemperature: Temperature,
    setCurrentWeather: {(weather: [Humidity,Temperature,boolean]): void}
}

export const defaultHumidity: Humidity = {
    relativeHumidity: 0,
    readingAt: new Date()
}
export const defaultTemperature: Temperature = {
    celsius: 0,
    readingAt: new Date()
}

const CurrentWeatherState = createContext<CurrentWeatherContext>({
    initial: false,
    currentHumidity: defaultHumidity,
    currentTemperature: defaultTemperature,
    setCurrentWeather: weather => {}
});

export const useCurrentWeatherContext = (): CurrentWeatherContext => useContext(CurrentWeatherState);

const CurrentWeatherProvider = (props: {children: any}) =>{
    const [[currentHumidity, currentTemperature, initial], setCurrentWeather] = useState<[Humidity, Temperature, boolean]>([defaultHumidity, defaultTemperature, false]);

    return (<CurrentWeatherState.Provider value={{
        initial: initial,
        currentHumidity: currentHumidity,
        currentTemperature: currentTemperature,
        setCurrentWeather: setCurrentWeather
    }}>
        {props.children}
    </CurrentWeatherState.Provider>);
}

export default CurrentWeatherProvider;