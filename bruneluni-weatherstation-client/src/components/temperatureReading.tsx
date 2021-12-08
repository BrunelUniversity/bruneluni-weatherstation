import React from "react";
import BaseReading from "./baseReading";

export interface Temperature{
    readingAt: Date,
    celsius: number
}

const TemperatureReading = ( { readingAt, celsius }: Temperature ): JSX.Element => {
    return <BaseReading reading={{readingAt: readingAt, value: celsius}} readingText={"temperature"}></BaseReading>
}

export default TemperatureReading;