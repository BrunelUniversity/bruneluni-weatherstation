import React from "react";
import styled, {css} from "styled-components";

export interface BaseReading {
    readingAt: Date,
    value: number
}

const InlineText = css`
    margin:0px;
    display: inline-block;
    padding:1em;
    vertical-align: top;
    font-family: 'Open Sans', sans-serif;
    font-size: 16px;
    line-height: 28px;
`
const InlineParagraph = styled.p`
    ${InlineText};
    text-align:right;
    padding-right:2em;
`

const InlineHeading = styled.h1`
    ${InlineText};
    font-weight: bold;
`

const BaseReading = ( { reading, readingText }: { reading: BaseReading, readingText: string } ): JSX.Element => {
    return <div>
        <InlineHeading>{readingText}: </InlineHeading><InlineParagraph>{reading.value.toPrecision(4)}</InlineParagraph>
        <InlineHeading>at: </InlineHeading><InlineParagraph>{reading.readingAt.toLocaleTimeString()}</InlineParagraph>
    </div>
}

export default BaseReading;