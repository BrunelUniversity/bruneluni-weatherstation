import React from 'react';
import HumidityReading from "./components/humidityReading";
import TemperatureReading from "./components/temperatureReading";
import HumidityGraph from "./components/humidityGraph";
import styled from "styled-components";
import TemperatureGraph from "./components/temperatureGraph";
import TemperatureProvider from "./state/temperatureState";

const Container = styled.div`
  text-align: center;
`;


function App() {
    return (
        <TemperatureProvider>
        <Container className="App">
            <HumidityReading readingAt={new Date()} relativeHumidity={10.5436543322}/>
            <TemperatureReading readingAt={new Date()} celsius={10.29848342}/>
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
        </TemperatureProvider>
    );
}

export default App;
