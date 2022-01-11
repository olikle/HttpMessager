#!/usr/bin/python3
# coding: utf8
#
# import configparser
# https://www.codeproject.com/Articles/5319621/Configuration-Files-in-Python
#
# import logging
# https://docs.python.org/3/library/logging.html
# https://stackoverflow.com/questions/13733552/logger-configuration-to-log-to-file-and-print-to-stdout
#
# try/error
# https://docs.python.org/3/tutorial/errors.html
#
# method overload
# https://stackoverflow.com/questions/10202938/how-do-i-use-method-overloading-in-python
#

import configparser
import logging

configObject = None

def init(config_filename = None, log_filename = None):
    global configObject
    configObject = read_config(config_filename)
    init_logger(log_filename)

def init_logger(logfilename = None):
    FORMAT = '%(asctime)s %(levelname)s: %(message)s'
    if logfilename is not None:
        logging.basicConfig(
            level=logging.INFO,
            format=FORMAT,
            handlers=[
                logging.FileHandler(logfilename),
                logging.StreamHandler()
            ]
        )
    else:
        logging.basicConfig(
            level=logging.INFO,
            format=FORMAT
            )



# Method to read config file settings
def read_config(config_filename):
    config = configparser.ConfigParser()
    config.read(config_filename)
    return config

def get_config_item(section, item):
    try:
        ret = configObject[section][item]
#    except KeyError:
#        logError("Key: " + section + "/" + item + " not found")
#        ret = None
    except Exception as err:
        ret = None
        logError("Key " + section + "/" + item + " not found", err)

    return ret    

# Logging
def logInfo(*argv):
    logging.info( ", ".join( map(str, argv) ) )
def logError(message, err = None):
    if err is None:
        logging.error(message)
    else:
        logging.error(message)
        logging.error("{0},{1},{2}".format(message, err, type(err)))
        #logging.error(message, err, type(err))
