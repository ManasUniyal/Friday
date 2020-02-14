import requests

url = "https://wordsapiv1.p.rapidapi.com/words/example"

headers = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

response = requests.request("GET", url, headers=headers)

print(response.text)
