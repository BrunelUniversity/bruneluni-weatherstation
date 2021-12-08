import React from 'react';
import {Line, Pie, Scatter} from 'react-chartjs-2';
import { PointElement, CategoryScale, LinearScale, LineElement, Chart } from 'chart.js'
import {Temperature} from "./temperatureReading";
import styled from "styled-components";
import {Humidity} from "./humidityReading";
Chart.register(PointElement)
Chart.register(LineElement)
Chart.register(CategoryScale)
Chart.register(LinearScale)

const Container = styled.div`
  margin-left: auto;
  margin-right: auto;
  max-width: 800px;
`;

const WeatherGraph = ( props: { humReadings: Humidity[], tempReadings: Temperature[] } ): JSX.Element => {
    const humData = props.humReadings.map(reading => {
        return {x: reading.readingAt.getTime(), y: reading.relativeHumidity}
    });

    const tempData = props.tempReadings.map(reading => {
        return {x: reading.readingAt.getTime(), y: reading.celsius}
    });

    const data = {
        labels: ['Scatter'],
        datasets: [
            {
                label: 'Humidity Graph',
                fill: false,
                showLine: true,  //!\\ Add this line
                backgroundColor: 'rgba(75,192,192,0.4)',
                pointBorderColor: 'rgba(75,192,192,1)',
                pointBackgroundColor: '#fff',
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: 'rgba(75,192,192,1)',
                pointHoverBorderColor: 'rgba(220,220,220,1)',
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: tempData
            }
        ],
    };

    return <Container>
        <Scatter data={data} options={{scales:{
            xAxes:{
                ticks: {
                    callback: (label, index, labels) => new Date(label).toLocaleString()
                }
            }
        }}}/>
    </Container>
}

export default WeatherGraph;