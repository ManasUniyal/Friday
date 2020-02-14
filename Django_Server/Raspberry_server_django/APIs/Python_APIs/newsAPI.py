# importing the requests library 
import requests 
import json

URL = "https://newsapi.org/v2/top-headlines"

API_KEY="56e83b7a47de43c6bb6e3cf5b61813a0"
# defining a params dict for the parameters to be sent to the API 
PARAMS = {'sources':'google-news','apiKey':API_KEY,'pageSize':2} 

# sending get request and saving the response as response object 
r = requests.get(url = URL, params = PARAMS) 

# extracting data in json format 
data = r.json() 
news = data['articles']
return_news = ""
for n in news:
	return_news=return_news+n['title']
	return_news=return_news+"#"+n['content']
	return_news=return_news+"#"+n['urlToImage']
	return_news=return_news+"#"
	
	
print(return_news)

