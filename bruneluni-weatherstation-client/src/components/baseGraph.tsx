import React, {useEffect} from 'react';
import {Line, Pie, Scatter} from 'react-chartjs-2';
import {PointElement, CategoryScale, LinearScale, LineElement, Chart, ChartData} from 'chart.js'
import styled from "styled-components";
import {BaseReading} from "./baseReading";
import moment from "moment";
Chart.register(PointElement)
Chart.register(LineElement)
Chart.register(CategoryScale)
Chart.register(LinearScale)

const Container = styled.div`

  max-width: 500px;
  margin-top: 5px;
  margin-bottom: 50px;
  box-shadow: 0 0px 7.5px rgb(0 0 0 / 0.10);
  padding: 25px;
  border-radius: 5px;
  background-color: white;
`;
const BaseGraph = ( props: { readings: BaseReading[], title: string } ): JSX.Element => {
    const readingData = props.readings.map(reading => {
        return {x: new Date(reading.readingAt).getTime(), y: reading.value}
    });

    useEffect(()=>{
        console.log("graph rendered")
    })

    const data: ChartData<"scatter", {x: number, y: number}[], string> = {
        labels: ['Scatter'],
        datasets: [
            {
                label: props.title + " Graph",
                fill: true,
                showLine: true,
                borderJoinStyle: "round",
                backgroundColor: '#aaafff',
                pointBorderColor: '#999999',
                borderColor: '#999999',
                borderWidth: 0.5,
                hoverBackgroundColor: '#ffffff',
                pointBackgroundColor: '#faf7f7',
                pointHoverBackgroundColor: '#faf7f7',
                pointHoverBorderColor: '#faf7f7',
                pointHoverBorderWidth: 1,
                pointHoverRadius: 5,
                pointRadius: 0,
                pointBorderWidth: 0,
                pointHitRadius: 0,
                data: readingData
            }
        ],
    };

    return <Container>
        <Scatter data={data} options={{
            maintainAspectRatio: true,
            scales:{
                yAxes:{
                    grid:{
                        color: "#ffffffff"
                    }
                },
                xAxes:{
                    grid: {
                        color: "#ffffffff"
                    },
                    ticks: {
                        callback: (label, index, labels) => {
                            const date = new Date(label);
                            return date.toLocaleString()
                        }
                    }
                }
            },
            backgroundColor: '#ffffff',
            color: '#ffffff'}}/>
    </Container>
}

export default BaseGraph;
