from django.shortcuts import render
import requests
import json

# Create your views here.

headers = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

def wordMeaning(request):
	if request.method == 'GET':
		wordToSearch = request.GET['word']
		url_meaning = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch
		response_meaning = requests.request("GET", url_meaning, headers=headers)
		json_data_meaning = json.loads(response_meaning.text)
		word = json_data_meaning['word']
		definition = json_data_meaning['results'][0]['definition']
		url_example = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch + "/examples"
		response_example = requests.request("GET", url_example, headers=headers)
		json_data_example = json.loads(response_example.text)
		example = json_data_example['examples'][0]
		text = word+'#'+definition+'#'+example
		return render(request, 'base.html', {'concatenated_string' : text}) 

def news(request):
	if request.method == 'GET':
		url_news = "https://newsapi.org/v2/top-headlines"
		no_of_news = 2
		PARAMS = {'sources':'google-news','apiKey':"56e83b7a47de43c6bb6e3cf5b61813a0",'pageSize':no_of_news} 
		r = requests.get(url = url_news, params = PARAMS)
		data = r.json() 
		news = data['articles']
		return_news = ""
		for n in news:
			title = n['title'][:min(40, len(n['title']))]
			content = n['content'][:min(60, len(n['content']))]
			imageURL = n['urlToImage'] 
			return_news += title+'#'+content+"#"+imageURL+'#'
		return render(request, 'news.html', {'count' : no_of_news, 'concatenated_string' : return_news}) 

