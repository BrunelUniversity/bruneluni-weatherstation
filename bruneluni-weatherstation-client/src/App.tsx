import React from 'react';
import HumidityGraph from "./components/humidityGraph";
import styled from "styled-components";
import TemperatureGraph from "./components/temperatureGraph";
import CurrentWeatherProvider from "./states/currentWeatherState";
import CurrentWeatherReadings from "./components/currentWeatherReadings";
import CurrentWeatherStateManager from "./states/currentWeatherStateManager";
import WeatherReadingsProvider from './states/weatherReadingsState';
import WeatherReadingsStateManager from "./states/weatherReadingsStateManager";
import WeatherReadings from "./components/weatherReadings";
import NavBar from "./components/navBar";

const Container = styled.div`
  margin-left: 10%;
  margin-top: 125px;
  background-color: aliceblue;
  color: #777777;
`;

function App() {
    return (
        <div>
            <WeatherReadingsProvider>
                <CurrentWeatherProvider>
                    <CurrentWeatherStateManager/>
                    <WeatherReadingsStateManager/>
                    <NavBar/>
                    <Container className="App">
                        <CurrentWeatherReadings/>
                        <WeatherReadings/>
                    </Container>
                </CurrentWeatherProvider>
            </WeatherReadingsProvider>
        </div>
    );
}

export default App;
