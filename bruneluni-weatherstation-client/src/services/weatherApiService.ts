import {Temperature} from "../components/temperatureReading";
import {defaultHumidity, defaultTemperature} from "../states/currentWeatherState";
import {Humidity} from "../components/humidityReading";

const baseUrl = "https://92.233.227.46/weather-station/api/";

export const getTemperatureReadings = ( callback:{(data: any):void} ) => getFromApi("temperature", callback)
export const getHumidityReadings = ( callback:{(data: any):void} ) => getFromApi("humidity", callback)
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

const getFromApi = (url: string, callback:{(data: any):void}) => {
    console.log(baseUrl+url)
    fetch(baseUrl+url)
        .then(response => response.json())
        .then(data => callback(data))
        .catch(e => console.log(e))

}
