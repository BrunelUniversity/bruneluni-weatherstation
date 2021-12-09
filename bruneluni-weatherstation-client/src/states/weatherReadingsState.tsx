import React, {createContext, useContext, useState} from "react";
import { Humidity } from "../components/humidityReading";
import {Temperature} from "../components/temperatureReading";

interface WeatherReadingsContext{
    initial: boolean,
    humidityReadings: Humidity[],
    tempReadings: Temperature[],
    setCurrentWeatherReadings: {(weather: [Humidity[],Temperature[],boolean]): void}
}

const WeatherReadingsContext = createContext<WeatherReadingsContext>({
    initial: false,
    humidityReadings: [],
    tempReadings: [],
    setCurrentWeatherReadings: weather => {}
});

export const useWeatherReadingsContext = (): WeatherReadingsContext => useContext(WeatherReadingsContext);

const WeatherReadingsProvider = (props: {children: any}) =>{
    const [[humidityReadings, tempReadings, initial], setReadings] = useState<[Humidity[], Temperature[], boolean]>([[], [], false]);

    return (<WeatherReadingsContext.Provider value={{
        initial: initial,
        humidityReadings: humidityReadings,
        tempReadings: tempReadings,
        setCurrentWeatherReadings: setReadings
    }}>
        {props.children}
    </WeatherReadingsContext.Provider>);
}

export default WeatherReadingsProvider;