export const getTemperatureReadings = ( callback:{(data: any):void} ) => getFromApi("temperature", callback)
export const getHumidityReadings = ( callback:{(data: any):void} ) => getFromApi("humidity", callback)
export const getCurrentTemperature = ( callback:{(data: any):void} ) => getFromApi("temperature/current", callback)
export const getCurrentHumidity = ( callback:{(data: any):void} ) => getFromApi("humidity/current", callback)

const getFromApi = (url: string, callback:{(data: any):void}) => {
    fetch(process.env.BE_API_URL?.toString()+url)
        .then(response => response.json())
        .then(data => callback(data))
        .catch(e => console.log(e))
}