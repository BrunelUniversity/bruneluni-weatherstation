import React from "react";

export interface Humidity {
    readingAt: Date,
    relativeHumidity: number
}

const HumidityReading = ( humidityReading: Humidity ): JSX.Element => {
    return <div>
        <h1>humidity reading is</h1>
        <p>{humidityReading.relativeHumidity.toPrecision(2)}</p>
        <h1>at time</h1>
        <p>{humidityReading.readingAt.getUTCDate().toPrecision(2)}</p>
    </div>
}

export default HumidityReading;