import {Temperature} from "../components/temperatureReading";
import {defaultHumidity, defaultTemperature} from "../states/currentWeatherState";
import {Humidity} from "../components/humidityReading";

//console.log(env);
const baseUrl = process.env.REACT_APP_API_URL_BE;

export const getCurrentAndUpdateState = ( callback:{(data: [Temperature, Humidity]): void }) => {
    let humidity: Humidity = defaultHumidity;
    let temperature: Temperature = defaultTemperature;
    getFromApi("humidity/current", x => {
        humidity = x;
        getFromApi("temperature/current", x => {
            temperature = x;
            callback([temperature, humidity])
        })
    })
}

export const getReadingsAndUpdateState = ( callback:{(data: [Temperature[], Humidity[]]): void }) => {
    let humidityReadings: Humidity[] = [];
    let temperatureReadings: Temperature[] = [];
    getFromApi("humidity", x => {
        humidityReadings = x;
        getFromApi("temperature", x => {
            temperatureReadings = x;
            callback([temperatureReadings, humidityReadings])
        })
    })
}

const getFromApi = (url: string, callback:{(data: any):void}) => {
    fetch(baseUrl+url)
        .then(response => response.json())
        .then(data => callback(data))
        .catch(e => console.log(e))

}
