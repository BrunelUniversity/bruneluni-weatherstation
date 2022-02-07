import React, {useEffect} from "react";
import styled, {css} from "styled-components";
import {useCurrentWeatherContext} from "../states/currentWeatherState";
import Loader from "react-loader-spinner";

export interface BaseReading {
    readingAt: Date,
    value: number
}

const InlineText = css`
    margin:0px;
    display: inline-block;
    padding-right:1em;
    vertical-align: top;
    font-size: 16px;
    line-height: 28px;
`
const InlineParagraph = styled.p`
    ${InlineText};
    text-align: right;
    font-weight: lighter;
    animation-name: pulse;
    animation-duration: 1s;
`

const InlineHeading = styled.h1`
    ${InlineText};
    font-weight: lighter;
`

const BaseReading = ( { reading, readingText, key }: { reading: BaseReading, readingText: string, key: string } ): JSX.Element => {
    useEffect(()=>{
        console.log("reading rendered")
    })

    return <div>
        <InlineHeading>{readingText}: </InlineHeading><InlineParagraph className="pulsing" key={new Date().getSeconds()}>{reading.value.toPrecision(4)}</InlineParagraph>
        <InlineHeading>at: </InlineHeading><InlineParagraph className="pulsing" key={new Date().getSeconds()+1}>{new Date(reading.readingAt).toLocaleTimeString()}</InlineParagraph>
    </div>
}

export default BaseReading;