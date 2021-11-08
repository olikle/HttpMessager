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

loop = None

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
    print("buttonpressed")
    r = requests.get('http://192.168.35.30/')
    print(r)

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
        GPIO.add_event_detect(4, GPIO.RISING, callback=buttonpressed_callback, bouncetime=500)
        
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


