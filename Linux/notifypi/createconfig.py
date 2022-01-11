#!/usr/bin/python3
# coding: utf8
#
# https://www.codeproject.com/Articles/5319621/Configuration-Files-in-Python
#

import configparser

# CREATE OBJECT
config_file = configparser.ConfigParser()

# FILE NAME
config_filename = "notifyOnRing.ini"
# ADD SECTION
config_file.add_section("RPCSettings")
# ADD SETTINGS TO SECTION
config_file.set("RPCSettings", "port", "30120")
config_file.set("RPCSettings", "notifyips", "192.168.0.121")

# ADD NEW SECTION AND SETTINGS
config_file["Logger"]={
        "LogFilePath":".",
        "LogFileName" : "notifyOnRing.log",
        "LogLevel" : "Info"
        }

# UPDATE A FIELD VALUE
config_file["Logger"]["LogLevel"]="Debug"
 
# ADD A NEW FIELD UNDER A SECTION
config_file["Logger"].update({"Format":"(message)"})

# DELETE A FIELD IN THE SECTION
config_file.remove_option('Logger', 'Format')
 

# DELETE A SECTION
config_file.add_section("SectionToDelete")
config_file.remove_section('SectionToDelete')



# SAVE CONFIG FILE
with open(config_filename, 'w') as configfileObj:
    config_file.write(configfileObj)
    configfileObj.flush()
    configfileObj.close()

print("Config file '",config_filename,"' created")

# PRINT FILE CONTENT
read_file = open(config_filename, "r")
content = read_file.read()
print("Content of the config file are:\n")
print(content)
read_file.flush()
read_file.close()