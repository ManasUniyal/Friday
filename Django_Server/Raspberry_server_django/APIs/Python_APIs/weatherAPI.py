import requests	
import json

headers_weather = {
    'x-rapidapi-host': "weatherbit-v1-mashape.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

url = "https://weatherbit-v1-mashape.p.rapidapi.com/current"

querystring = {"lang":"en","lon":"81.846313","lat":"25.435801"}

response = requests.request("GET", url, headers=headers_weather, params=querystring)
json_data_weather = json.loads(response.text)
longitude = json_data_weather['data'][0]['lon']
latitude = json_data_weather['data'][0]['lat']
city = json_data_weather['data'][0]['city_name']
temperature = json_data_weather['data'][0]['temp']
weather = json_data_weather['data'][0]['weather']['description']
resultString = str(longitude)+'#'+str(latitude)+'#'+city+'#'+str(temperature)+'#'+weather
print (resultString)
