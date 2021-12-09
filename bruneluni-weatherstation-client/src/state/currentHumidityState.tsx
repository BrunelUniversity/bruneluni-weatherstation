import React, {createContext, useContext, useState} from "react";
import { Humidity } from "../components/humidityReading";

interface CurrentHumidityContext{
    currentHumidity: Humidity,
    setCurrentHumidity: {(Humidity: Humidity): void}
}

const defaultHumidity: Humidity = {
    relativeHumidity: 0,
    readingAt: new Date()
}

const CurrentHumidityContext = createContext<CurrentHumidityContext>({
    currentHumidity: {
        relativeHumidity: 0,
        readingAt: new Date()
    },
    setCurrentHumidity: Humidity => {

    }
});

export const useCurrentHumidityContext = (): CurrentHumidityContext => useContext(CurrentHumidityContext);

const CurrentHumidityProvider = (props: {children: any}) =>{
    const [currentHumidity, setCurrentHumidity] = useState<Humidity>(defaultHumidity);

    return (<CurrentHumidityContext.Provider value={{
        currentHumidity: currentHumidity,
        setCurrentHumidity: setCurrentHumidity
    }}>
        {props.children}
    </CurrentHumidityContext.Provider>);
}

export default CurrentHumidityProvider;