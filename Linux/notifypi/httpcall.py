#!/usr/bin/python3
# coding: utf8
#
# sudo python -m pip install asyncio
#
# https://www.python-forum.de/viewtopic.php?t=41836
# sudo apt-get install python3-dev
#
# https://2.python-requests.org/en/master/user/quickstart/#custom-headers
# pip install requests
#
#

import asyncio
import sys
import requests
import json
import time
#from datetime import datetime
import datetime
import helper

loop = None
lastTime = None

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
        helper.init("notifyOnRing.ini", "httpcall.log")
    #try:
        print("first sleep 3 seconds")
        #time.sleep(3)
        configPortNo = helper.get_config_item("RPCSettings", "Port")
        configNotifyIps = helper.get_config_item("RPCSettings", "notifyips")

        if configPortNo == None or configNotifyIps == None:
            print("portNo or notifyips missing")
            sys.exit()

        print("portNo",configPortNo)
        print("confignotifyips",configNotifyIps)

        notifyIps = configNotifyIps.split(",")
        print("notifyips",notifyIps)

        now = datetime.datetime.now()
        current_date = now.strftime("%d.%m.%Y")
        current_time = now.strftime("%H:%M:%S")
        timeToCheck = now + datetime.timedelta(seconds=3)

        lastTime = None
        lastTime = now

        if lastTime is not None and lastTime < timeToCheck:
           print ("time is to short - exit...")
        else:
           print ("time is ok")

        requestJsonRPC = RequestJsonRPC("1", "ringpi", "ping from ringpi (" + current_date + ", " + current_time + ")...", "SendMessage")

        for ipAddress in notifyIps:
            url = "http://" + ipAddress + ":" + configPortNo + "/jsonrpc"
            helper.logInfo("request", url, requestJsonRPC)
            r = requests.post(url, data = json.dumps(requestJsonRPC) )
            helper.logInfo("response", r.reason, r.text)
            #print(r)
            #print(r.text)


    #except KeyboardInterrupt:
    #    print("Process interrupted")
    #except :
    #    print("Error:", sys.exc_info()[0])
    #finally:
    #    print("correct close")


