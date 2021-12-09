import React, {createContext, useContext, useState} from "react";
import {Temperature} from "../components/temperatureReading";

interface CurrentTemperatureContext{
    currentTemperature: Temperature,
    setCurrentTemperature: {(temperature: Temperature): void}
}

const defaultTemperature: Temperature = {
    celsius: 0,
    readingAt: new Date()
}

const CurrentTemperatureContext = createContext<CurrentTemperatureContext>({
    currentTemperature: {
        celsius: 0,
        readingAt: new Date()
    },
    setCurrentTemperature: temperature => {

    }
});

export const useCurrentTemperatureContext = (): CurrentTemperatureContext => useContext(CurrentTemperatureContext);

const CurrentTemperatureProvider = (props: {children: any}) =>{
    const [currentTemperature, setCurrentTemperature] = useState<Temperature>(defaultTemperature);

    return (<CurrentTemperatureContext.Provider value={{
        currentTemperature: currentTemperature,
        setCurrentTemperature: setCurrentTemperature
    }}>
        {props.children}
    </CurrentTemperatureContext.Provider>);
}

export default CurrentTemperatureProvider;