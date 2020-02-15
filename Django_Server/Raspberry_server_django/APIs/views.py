from __future__ import unicode_literals
from django.shortcuts import render
from PIL import Image
import requests
import json
import urllib.request
from bs4 import BeautifulSoup
import glob
import youtube_dl
import webbrowser
import os
import shutil
import socket
import sys
import cv2

# Create your views here.

headers_words = {
    'x-rapidapi-host': "wordsapiv1.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

headers_weather = {
    'x-rapidapi-host': "weatherbit-v1-mashape.p.rapidapi.com",
    'x-rapidapi-key': "fce152fd01msh1c6216ea9455f5ap16535djsn0edbc4de3361"
    }

def wordMeaning(request):
	if request.method == 'GET':
		wordToSearch = request.GET['word']
		url_meaning = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch
		response_meaning = requests.request("GET", url_meaning, headers=headers_words)
		json_data_meaning = json.loads(response_meaning.text)
		word = json_data_meaning['word']
		if (len(json_data_meaning['results'][0]['definition'])) :
			definition = json_data_meaning['results'][0]['definition']
		url_example = "https://wordsapiv1.p.rapidapi.com/words/" + wordToSearch + "/examples"
		response_example = requests.request("GET", url_example, headers=headers_words)
		json_data_example = json.loads(response_example.text)
		example = ""
		if len(json_data_example['examples']) > 0:
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
		return render(request, 'base_with_count.html', {'count' : no_of_news, 'concatenated_string' : return_news}) 

def youtube(request):

	if request.method == 'GET':
		
		searchWord = request.GET['word']
		if ((searchWord.startswith("'") and searchWord.endswith("'")) or searchWord.startswith('"') and searchWord.endswith('"')):
			searchWord = searchWord[1:-1]
		cwd = os.getcwd()
		textToSearch = searchWord
		query = urllib.parse.quote(textToSearch)
		url = "https://www.youtube.com/results?search_query=" + query
		response = urllib.request.urlopen(url)
		html = response.read()
		soup = BeautifulSoup(html, 'html.parser')
		for vid in soup.findAll(attrs={'class':'yt-uix-tile-link'},limit=1):
			print('https://www.youtube.com' + vid['href'])
			url = ('https://www.youtube.com/' + vid['href'])
			print('Done!')

		ydl_opts = {'format': 'mp4'}
		with youtube_dl.YoutubeDL(ydl_opts) as ydl:
			ydl.download([url])

		list_of_files = glob.glob(os.getcwd()+"/*") # * means all if need specific format then *.csv
		latest_file = max(list_of_files, key=os.path.getctime)
		print(latest_file)
		os.rename(latest_file[latest_file.rfind("/")+1:], textToSearch+".mp4") 
		shutil.move("./"+textToSearch+".mp4", "./videos/"+textToSearch+".mp4")
		
		UDP_IP = "10.0.0.11"
		UDP_PORT = 5065

		sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
		txt = "1#" + textToSearch + ".mp4"
		sock.sendto( txt.encode(), (UDP_IP, UDP_PORT))
		
		return render(request, 'base.html')

def weatherReport(request):
	if request.method == 'GET':
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
		return render(request, 'base.html', {'concatenated_string': resultString})

def OCR(request):

	def detect_document(path):
		"""Detects document features in an image."""
		from google.cloud import vision
		import io
		client = vision.ImageAnnotatorClient()

		with io.open(path, 'rb') as image_file:
			content = image_file.read()

		image = vision.types.Image(content=content)

		response = client.document_text_detection(image=image)

		resultString = str()

		for page in response.full_text_annotation.pages:
			for block in page.blocks:
				print('\nBlock confidence: {}\n'.format(block.confidence))
				
				for paragraph in block.paragraphs:
					print('Paragraph confidence: {}'.format(
						paragraph.confidence))

					for word in paragraph.words:
						resultString += word+"#"
						word_text = ''.join([
							symbol.text for symbol in word.symbols
						])
						print('Word text: {} (confidence: {})'.format(
							word_text, word.confidence))

		if response.error.message:
			raise Exception(
				'{}\nFor more info on error messages, check: '
				'https://cloud.google.com/apis/design/errors'.format(
					response.error.message))

		import os
		os.remove("live.jpeg")
		print("File Removed!")

	camera = cv2.VideoCapture(0)
	return_value, image = camera.read()
	file = 'live.jpeg'
	cv2.imwrite(file, image)
	detect_document("live.jpeg")

	return render(request, 'base.html', {'concatenated_string': resultString}) 	


