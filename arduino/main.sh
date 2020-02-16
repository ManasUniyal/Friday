#!/bin/bash
python3 main.py $1
arduino --upload program.nso --port $2