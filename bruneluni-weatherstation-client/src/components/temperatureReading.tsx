import React from "react";
import styled, {css} from "styled-components";
import BaseReading from "./baseReading";

export interface Temperature{
    readingAt: Date,
    celsius: number
}

const TemperatureReading = ( { readingAt, celsius }: Temperature ): JSX.Element => {
    return <BaseReading readingAt={readingAt} value={celsius} readingText={"temperature"}></BaseReading>
}

export default TemperatureReading;