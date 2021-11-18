#!/usr/bin/python3
# coding: utf8
#
# sudo python -m pip install asyncio
#
# https://www.python-forum.de/viewtopic.php?t=41836
# sudo apt-get install python3-dev python3-rpi.gpio
# sudo apt-get install python3-rpi.gpio
#
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



def message_manager_f():
    print (":P message_manager_f()")

def motion_sensor(self, message_manager_f):
    print ("motion_sensor")
    if loop is None:
        print(":(")
        return       # should not come to this
    # this enqueues a call to message_manager_f() 
    loop.call_soon_threadsafe(self.message_manager_f)

def buttonpressed_callback(self):
    print("The bell rings...")
    now = datetime.now()
    current_time = now.strftime("%H:%M:%S")

    requestJsonPRC = RequestJsonRPC("1", "ringpi", "The Bell rings... (" + current_time + ")...", "SendMessage")

    ipList = ["192.168.0.120", "192.168.0.121"]
    for ipAddress in ipList:
       print("call...", ipAddress, requestJsonPRC)
       #r = requests.post(ipAddress + "jsonrpc", data = requestJsonPRC)
       r = requests.post("http://" + ipAddress + ":30120" + "/jsonrpc", data = json.dumps(requestJsonPRC) )
       #r = requests.get(ipAddress + "message?text=Hallo")
       print(r.text)


# this is the primary thread mentioned in Part 2
if __name__ == '__main__':
    try:
        print ("wait for GPIO...")
        # setup the GPIO
        GPIO.setwarnings(True)
        GPIO.setmode(GPIO.BCM)
        #GPIO.setmode(GPIO.BOARD)
        #GPIO.setup(4, GPIO.IN, pull_up_down = GPIO.PUD_UP) # adjust the PULL UP/PULL DOWN as applicable
        GPIO.setup(4, GPIO.IN, pull_up_down = GPIO.PUD_UP) # adjust the PULL UP/PULL DOWN as applicable
        #GPIO.add_event_detect(4, GPIO.RISING, callback=lambda x: motion_sensor(message_manager_f), bouncetime=500)
        #GPIO.add_event_callback(4, my_callback)
        GPIO.add_event_detect(4, GPIO.RISING, callback=buttonpressed_callback, bouncetime=1000)
        
        # run the event loop
        loop = asyncio.get_event_loop()
        loop.run_forever()
    except KeyboardInterrupt:
        print("Process interrupted")
    except :
        print("Error:", sys.exc_info()[0])
    finally:
        print("correct close")
        if loop is not None:
            loop.close()
        # cleanup
        GPIO.cleanup()


