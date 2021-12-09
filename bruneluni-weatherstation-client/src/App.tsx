import React from 'react';
import HumidityReading from "./components/humidityReading";
import TemperatureReading from "./components/temperatureReading";
import HumidityGraph from "./components/humidityGraph";
import styled from "styled-components";
import TemperatureGraph from "./components/temperatureGraph";
import CurrentTemperatureProvider from "./state/currentTemperatureState";
import CurrentHumidityProvider from "./state/currentHumidityState";
import CurrentWeatherReadings from "./components/currentWeatherReadings";

const Container = styled.div`
  text-align: center;
`;


function App() {
    return (
        <div>
            <CurrentHumidityProvider>
                <CurrentTemperatureProvider>
                    <Container className="App">
                        <CurrentWeatherReadings/>
                            <HumidityGraph readings={[
                                {readingAt: new Date(2021, 8, 12, 2, 2, 2), relativeHumidity: 33.2},
                                {readingAt: new Date(2021, 8, 12, 3, 2, 2), relativeHumidity: 32.2},
                                {readingAt: new Date(2021, 8, 13, 2, 2, 2), relativeHumidity: 35.2},
                                {readingAt: new Date(2021, 8, 13, 9, 2, 2), relativeHumidity: 40.2}
                            ]}/>
                            <TemperatureGraph readings={[
                                {readingAt: new Date(2021, 8, 12, 1, 2, 2), celsius: 18.2},
                                {readingAt: new Date(2021, 8, 12, 9, 2, 2), celsius: 19.2},
                                {readingAt: new Date(2021, 8, 13, 1, 2, 2), celsius: 27.2},
                                {readingAt: new Date(2021, 8, 13, 2, 2, 2), celsius: 10.2}
                            ]}/>
                    </Container>
                </CurrentTemperatureProvider>
            </CurrentHumidityProvider>
        </div>
    );
}

export default App;
