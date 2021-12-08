import React, {createContext, useContext, useEffect, useState} from "react";
import {Temperature} from "../components/temperatureReading";
import { clearInterval } from 'timers';

interface TemperatureContext{
    readings?: Temperature[],
    latest?: Temperature,
}

const TemperatureReadingsContext = createContext<TemperatureContext>({
    readings: undefined,
    latest: undefined
});

export const useTemperatureContext = (): TemperatureContext => useContext(TemperatureReadingsContext);

const TemperatureProvider = (props: {children: any}) =>{
    const [readings, setReadings] = useState<Temperature[]>();
    const [latest, setLatest] = useState<Temperature>();
    useEffect(()=>{
        if(latest !== undefined){
            console.log(latest.celsius)
            console.log(latest.readingAt)
            console.log(readings?.map(x => {return{celsius: x.celsius, readingAt: x.readingAt}}))
        }
        const id = setInterval(()=>{
            getFromApiAndLog("temperature/current", x=>{
                setLatest(x)
            })
            getFromApiAndLog("temperature", x=>{
                setReadings(x)
            })
        },2000)
        return function cleanup() {
            clearTimeout(id);
        };
    })
    return(
        <TemperatureReadingsContext.Provider value={{
            readings: readings,
            latest: latest
        }}>
            {props.children}
        </TemperatureReadingsContext.Provider>
    );
}

export default TemperatureProvider;


const getFromApiAndLog = async (url: string, callback: {(data: any): void}) => {
    fetch("https://92.233.227.46/weather-station/api/"+url)
        .then(x => x.json())
        .then(x => callback(x));
}