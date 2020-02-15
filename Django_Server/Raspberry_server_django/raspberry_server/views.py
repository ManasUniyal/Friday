from django.shortcuts import render
import subprocess
from pythonScripts import raspberryCameraCapture

def call(request):
	if request.method == 'POST':
		call_GSM(int(request.POST['phoneNumber']))

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
		count = 0
		for image in f:
			count += 1
			image_string += image.strip() + "#"
		return render(request, 'base_with_count.html', {'count' : count, 'concatenated_string' : image_string})

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

def reminder(request):
	if request.method == 'GET':
		reminderTime = request.GET['time']
		notification = request.GET['notification']


def call_GSM(phoneNumber):
	pass		

