import React from "react";
import {Humidity} from "./humidityReading";
import BaseGraph from "./baseGraph";

const HumidityGraph = ( props: { readings: Humidity[] } ): JSX.Element => {
    return <BaseGraph readings={props.readings.map(x => {
        return {value: x.relativeHumidity, readingAt: x.readingAt}
    })} title={"Humidity"}/>
}

export default HumidityGraph;