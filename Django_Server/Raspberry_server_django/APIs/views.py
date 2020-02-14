from django.shortcuts import render
import requests
import json

# Create your views here.

url = "https://wordsapiv1.p.rapidapi.com/words/beautiful"

headers = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

def wordMeaning(request):
	response = requests.request("GET", url, headers=headers)
	json_data = json.loads(response.text)
	word = json_data['word']
	definition = json_data['results'][0]['definition']
	part_of_speech = json_data['results'][0]['partOfSpeech']
	text = word+'#'+definition+"#"+part_of_speech
	return render(request, 'base.html', {'concatenated_string' : text}) 
