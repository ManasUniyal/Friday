import requests
import json

headers = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

wordToSearch = 'example'
url_meaning = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch
response_meaning = requests.request("GET", url_meaning, headers=headers)
json_data_meaning = json.loads(response_meaning.text)
word = json_data_meaning['word']
definition = json_data_meaning['results'][0]['definition']
url_example = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch + "/examples"
response_example = requests.request("GET", url_example, headers=headers)
json_data_example = json.loads(response_example.text)
example = json_data_example['examples'][0]
text = word+'#'+definition+"#"+example
print(text)