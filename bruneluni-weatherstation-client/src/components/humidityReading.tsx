import React from "react";
import styled, {css} from "styled-components";
import BaseReading from "./baseReading";

export interface Humidity{
    readingAt: Date,
    relativeHumidity: number
}

const HumidityReading = ( { readingAt, relativeHumidity }: Humidity ): JSX.Element => {
    return <BaseReading reading={{readingAt: readingAt, value: relativeHumidity}} readingText={"humidity"} key={""}></BaseReading>
}

export default HumidityReading;