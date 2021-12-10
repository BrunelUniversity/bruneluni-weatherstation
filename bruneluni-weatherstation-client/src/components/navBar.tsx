import React from "react";
import BaseReading from "./baseReading";
import styled from "styled-components";

const Heading = styled.h1`
    font-weight: lighter;
    color: white;
    font-size: x-large;
`

const Container = styled.div`
  background-color: cornflowerblue;
  text-align: center;
  padding: 10px;
  box-shadow: 0 0px 7.5px rgb(0 0 0 / 0.35);
  top: 0;
  width 100%;
  position: fixed;
`;

const NavBar = ( ): JSX.Element => {
    return <Container>
        <Heading>
            Weather App
        </Heading>
    </Container>
}

export default NavBar;