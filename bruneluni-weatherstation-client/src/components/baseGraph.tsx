import React from 'react';
import {Line, Pie, Scatter} from 'react-chartjs-2';
import {PointElement, CategoryScale, LinearScale, LineElement, Chart, ChartData} from 'chart.js'
import styled from "styled-components";
import {BaseReading} from "./baseReading";
Chart.register(PointElement)
Chart.register(LineElement)
Chart.register(CategoryScale)
Chart.register(LinearScale)

const Container = styled.div`
  margin-left: auto;
  margin-right: auto;
  max-width: 600px;
  margin-top: 50px;
  margin-bottom: 50px;
  box-shadow: 0 3px 10px rgb(0 0 0 / 0.15);
`;

const BaseGraph = ( props: { readings: BaseReading[], title: string } ): JSX.Element => {
    const readingData = props.readings.map(reading => {
        return {x: reading.readingAt.getTime(), y: reading.value}
    });

    const data: ChartData<"scatter", {x: number, y: number}[], string> = {
        labels: ['Scatter'],
        datasets: [
            {
                label: props.title + " Graph",
                fill: true,
                showLine: true,
                borderJoinStyle: "round",
                backgroundColor: '#aaafff',
                pointBorderColor: '#777777',
                borderColor: '#777777',
                borderWidth: 1,
                hoverBackgroundColor: '#ffffff',
                pointBackgroundColor: '#faf7f7',
                pointHoverBackgroundColor: '#faf7f7',
                pointHoverBorderColor: '#faf7f7',
                pointHoverBorderWidth: 1,
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointRadius: 3,
                pointHitRadius: 10,
                data: readingData,
                tension: 0.2
            }
        ],
    };

    return <Container>
        <Scatter data={data} options={{
            maintainAspectRatio: true,
            scales:{
                xAxes:{
                    ticks: {
                        callback: (label, index, labels) => new Date(label).toLocaleString()
                    }
                }
            },
            backgroundColor: '#ffffff',
            color: '#ffffff'}}/>
    </Container>
}

export default BaseGraph;