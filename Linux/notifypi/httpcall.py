#!/usr/bin/python3
# coding: utf8
#
# sudo python -m pip install asyncio
#
# https://www.python-forum.de/viewtopic.php?t=41836
# sudo apt-get install python3-dev python3-rpi.gpio
# sudo apt-get install python3-rpi.gpio
#
# https://2.python-requests.org/en/master/user/quickstart/#custom-headers
# pip install requests
# 
#

import asyncio
import RPi.GPIO as GPIO
import sys
import requests
import json
from datetime import datetime

loop = None

def RequestJsonRPC(jsonrpcId, resultFrom, resultMessage, jsonrpcMethod = None):
    jsonrpcReturn = {
        "jsonrpc": "2.0"          
    }
        # send id if exists, otherwise -1
    if jsonrpcId == None:
        jsonrpcReturn["id"] = -1
    else:
        jsonrpcReturn["id"] = jsonrpcId

    # send method only the exists
    if jsonrpcMethod != None:
        jsonrpcReturn["method"] = jsonrpcMethod

    jsonrpcParams = {
        "from": resultFrom,
        "message" : resultMessage         
    }
    jsonrpcReturn["params"] = jsonrpcParams

    return jsonrpcReturn

# this is the primary thread mentioned in Part 2
if __name__ == '__main__':
    try:
        #ipAddress = 'http://192.168.0.156:30120/'
        ipAddress = 'http://192.168.0.121:30120/'
        #ipAddress = 'http://192.168.0.156:8888/'
        #ipAddress = 'http://192.168.35.30:8888/'

        now = datetime.now()
        current_time = now.strftime("%H:%M:%S")

        requestJsonPRC = RequestJsonRPC("1", "ringpi", "ping from ring (" + current_time + ")...", "SendMessage")
        print("call...", ipAddress, requestJsonPRC)
        
        #r = requests.post(ipAddress + "jsonrpc", data = requestJsonPRC)
        r = requests.post(ipAddress + "jsonrpc", data = json.dumps(requestJsonPRC) )

        #r = requests.get(ipAddress + "message?text=Hallo")
        print(r.text)


        print(r)
    except KeyboardInterrupt:
        print("Process interrupted")
    except :
        print("Error:", sys.exc_info()[0])
    finally:
        print("correct close")


