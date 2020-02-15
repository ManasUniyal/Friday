import socket
import sys
import schedule
import time
import threading

UDP_IP = "10.0.0.11"
UDP_PORT = 5065

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

def job():
    txt = 'Show alarm'
    sock.sendto(txt.encode(), (UDP_IP, UDP_PORT))

def startSchedule():
	while 1:
		schedule.run_pending()
		time.sleep(1)
	
t1 = threading.Thread(target=startSchedule)
t1.start()    

f = open('/home/manas/Desktop/Friday/packetLogs/setAlarm.txt','r')
alarmTimes = f.read()

alarmTime = str()

for char in alarmTimes:
	alarmTime += char

str = alarmTime.split('"')
for i in range(1,len(str),2):
    schedule.every().day.at(str[i]).do(job)