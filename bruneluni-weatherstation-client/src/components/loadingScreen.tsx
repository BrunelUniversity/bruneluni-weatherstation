import React from "react";
import BaseReading from "./baseReading";
import styled from "styled-components";
import Loader from "react-loader-spinner";
import {useCurrentWeatherContext} from "../states/currentWeatherState";
import {useWeatherReadingsContext} from "../states/weatherReadingsState";

const Container = styled.div`
  background-color: cornflowerblue;
  text-align: center;
  top: 0;
  width: 100%;
  height: 100%;
  position: fixed;
  z-index: 10;
`;

const InnerContainer = styled.div`
  margin: 0;
  position: absolute;
  top: 50%;
  left: 50%;
  -ms-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
`;

const LoadingScreen = ( ): JSX.Element => {
    const initialCurrentWeather = useCurrentWeatherContext().initial;
    const initialWeatherReadings = useWeatherReadingsContext().initial;
    if( !( initialCurrentWeather && initialWeatherReadings ) ){
        return <Container>
            <InnerContainer>
                <Loader
                    type="Circles"
                    color='#f4fbff'
                    height={75}
                    width={75}
                    radius={1}
                    timeout={15000}
                />
            </InnerContainer>
        </Container>
    }
    else{
        return <React.Fragment/>
    }
}

export default LoadingScreen;