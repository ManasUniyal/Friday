import requests
import json

url = "https://wordsapiv1.p.rapidapi.com/words/beautiful"

headers = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

response = requests.request("GET", url, headers=headers)
json_data = json.loads(response.text)

print("Json data")
print("word = "+json_data['word'])
print("defintion = "+json_data['results'][0]['definition'])
print("part of speech = "+json_data['results'][0]['partOfSpeech'])
print(json_data)
