import React from 'react';
import './App.css';
import HumidityReading from "./components/humidityReading";
import TemperatureReading from "./components/temperatureReading";

function App() {
    return (
        <div className="App">
            <HumidityReading readingAt={new Date()} relativeHumidity={10.5436543322}/>
            <TemperatureReading readingAt={new Date()} celsius={10.29848342}/>
        </div>
    );
}

export default App;
