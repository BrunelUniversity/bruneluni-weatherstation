import React from "react";

export interface Temperature {
    readingAt: Date,
    celsius: number
}

const TemperatureReading = ( humidityReading: Temperature ): JSX.Element => {
    return <div>
        <h1>temperature reading is</h1>
        <p>{humidityReading.celsius.toPrecision(2)}</p>
        <h1>at time</h1>
        <p>{humidityReading.readingAt.getUTCDate().toPrecision(2)}</p>
    </div>
}

export default TemperatureReading;