import React from "react";
import BaseGraph from "./baseGraph";
import {Temperature} from "./temperatureReading";

const TemperatureGraph = (props: { readings: Temperature[] } ): JSX.Element => {
    return <BaseGraph readings={props.readings.map(x => {
        return {value: x.celsius, readingAt: x.readingAt}
    })} title={"Temperature"}/>
}

export default TemperatureGraph;