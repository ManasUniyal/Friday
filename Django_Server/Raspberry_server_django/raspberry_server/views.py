from django.shortcuts import render
from pythonScripts import raspberryCameraCapture
import subprocess
import os
import serial
import time

def OCR():

	def detect_document(path):
		global resultString
		"""Detects document features in an image."""
		from google.cloud import vision
		import io
		client = vision.ImageAnnotatorClient()
		with io.open(path, 'rb') as image_file:
			content = image_file.read()
		image = vision.types.Image(content=content)
		response = client.document_text_detection(image=image)
		for page in response.full_text_annotation.pages:
			for block in page.blocks:
				print('\nBlock confidence: {}\n'.format(block.confidence))
				
				for paragraph in block.paragraphs:
					print('Paragraph confidence: {}'.format(
						paragraph.confidence))
					for word in paragraph.words:
						word_text = ''.join([
							symbol.text for symbol in word.symbols
						])
						resultString += word_text+"#"
						print(word_text)
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

	resultString = str()
	camera = cv2.VideoCapture(0)
	return_value, image = camera.read()
	file = 'live.jpeg'
	cv2.imwrite(file, image)
	detect_document("live.jpeg")
	return resultString

def listSongs(request):
	if request.method == 'GET':
		subprocess.call('/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/get_songs_list.sh')
		f = open("/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/songs_list.txt","r")
		song_string = ""
		count = 0
		for song in f:
			song_string += song.strip() + "#"
			count += 1
		return render(request, 'base_with_count.html', {'count': count, 'concatenated_string' : song_string})

def listVideos(request):
	if request.method == 'GET':
		subprocess.call('/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/get_videos_list.sh')
		f = open("/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/videos_list.txt","r")
		video_string = ""
		count = 0
		for video in f:
			video_string += video.strip() + "#"
			count += 1
		return render(request, 'base_with_count.html', {'count' : count, 'concatenated_string' : video_string})

def listImages(request):
	if request.method == 'GET':
		subprocess.call('/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/get_images_list.sh')
		f = open("/home/manas/Desktop/Friday/Django_Server/Raspberry_server_django/shell_scripts/images_list.txt","r")
		image_string = ""
		for image in f:
			image_string += image.strip() + "#"
		return render(request, 'base.html', {'concatenated_string' : image_string})

def setAlarm(request):
	if request.method == 'GET':
		
		setAlarmTime = request.GET['alarmTime']
		f = open('/home/manas/Desktop/Friday/Send_Packets/packetLogs/setAlarm.txt','r')
		alarmTimes = f.read()

		alarmTime = str()
		alarmList = list()

		for char in alarmTimes:
				
			if char == '\n':
				print(alarmTime)
				alarmList.append(alarmTime)
				alarmTime = str()
			else:
				alarmTime += char

		if setAlarmTime not in alarmTime:
			f = open('/home/manas/Desktop/Friday/Send_Packets/packetLogs/setAlarm.txt','a')
			f.write(setAlarmTime)
		return render(request, 'base.html')

def captureImage(request):
	if request.method == 'GET':
		raspberryCameraCapture.func()
		return render(request, 'base.html', {'concatenated_string': 'Image captured successfully'})

def findQuestion(request):
	text = OCR()
	return render(request, 'base.html', {'concatenated_string': text})

def answerToQuestion(request):
	if request.method == 'GET':
		question = request.GET['question']
		os.system('python3 /home/manas/Desktop/Friday/scraping/t1.py question') 
		time.sleep(3)
		f = open('/home/manas/Desktop/Friday/scraping/answer.txt','r')
		solution = f.read()
		return render(request, 'base.html', {'concatenated_string': solution})

def findNumber(request):
	if request.method == 'GET':
		text = OCR()
		return render(request, 'base.html', {'concatenated_string': text})

def callNumber(request):
	if request.method == 'GET': 
		phoneNumber = request.GET['number']
		call_GSM(phoneNumber)

def call_GSM(phoneNumber):

	ser1=serial.Serial('/dev/ttyACM0',9600)
	ser1.write('call'.encode())
	time.sleep(5)
	ser1.write('ATD9057261430;'.encode())
		

