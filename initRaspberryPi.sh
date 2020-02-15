#!/bin/bash

cd ~/Desktop/Friday/Django_Server/Raspberry_server_django/videos &
python3 -m http.server 5002 &

cd ~/Desktop/Friday/Django_Server/Raspberry_server_django/songs &
python3 -m http.server 5003 &

cd ~/Desktop/Friday/Django_Server/Raspberry_server_django/images &
python3 -m http.server 5004 &

source ~/Desktop/Friday/Django_Server/raspberrypi_virtualenv/bin/activate &

cd ~/Desktop/Friday/Django_Server/Raspberry_server_django &
python3 manage.py runserver 0.0.0.0:8001 & 


