f = open('/home/manas/Desktop/Friday/packetLogs/setAlarm.txt','r')
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

print(alarmList)