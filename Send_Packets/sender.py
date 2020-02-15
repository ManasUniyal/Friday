import socket
import sys
import schedule
import time
import threading

UDP_IP = "10.0.0.11"
UDP_PORT = 5065

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

#sock.sendto( txt.encode(), (UDP_IP, UDP_PORT))

def job():
    print("I'm working...")

def startSchedule():
	while 1:
		schedule.run_pending()
		time.sleep(1)	
    
t1 = threading.Thread(target=startSchedule)
t1.start()
time1=input("Enter Time:")
time2 = input('Enter time')
schedule.every().day.at(time1).do(job)
schedule.every().day.at(time2).do(job)