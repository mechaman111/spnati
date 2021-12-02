# Playing SPNATI offline of phone and other LAN devices

Guide by Mattlau04, last updated 02/12/2021
    
--- 

!!!note
    You will need a computer for this, this guide is not about how to host SPNATI offline on a phone, but rather hosting it on your PC in a way that allows you to access it from your phone (or other LAN devices).
!!!note
    This is also not a guide on how to install SPNATI offline, for this guide i will assume you already have it downloaded on your pc
    
## Step 1: Installing python  
If you already have python 3.X installed, you can skip this step.
If not, go download it from [Python's website](https://www.python.org/downloads/)
!!!warning  
    During installation, make sure to check the box that says `Add to path`, if you didn't, you'll need to uninstall python and reinstall it, this time adding it to path.

## Step 2: Installing the required packages  
You need both the `bottle` and `gevent` librairies installed for this. 
To install them, just open a CMD window and type `pip install bottle gevent`, then press enter.
!!!note
    To open a CMD window, just hold the windows key, press r, type then `cmd` then enter

## Step 3: Editing the .py file
In your SPNATI offline install, there is a file called `offline_host.py`, and running it hosts a regular SPNATI instance that you can't access from other devices.
To change that, open the file in notepad (or any other text editor), then replace the line `host='localhost'` by `host = '0.0.0.0'`.
This allows other devices on the network to access the server. 
You then just need to save the file and run it, it should open a python window with a message that a server is running.

## Step 4: Accessing the server
To get the URL that you need to enter on your phone or other network device, you must get your local ip adress. If your local ip is `192.128.1.63`, then you must go to [`http://192.128.1.63:5000`](http://192.128.1.63:5000/) (assuming you didn't change the port in the py file)
!!!note  
     You can get your ip by using the `ipconfig` command in a cmd. If you don't know how to do that, you can find more details [here](https://www.whatismybrowser.com/detect/what-is-my-local-ip-address)

## About saves
**First of all, saves aren't cross-device. You can share your save string manually, but that's it.**

### Using the same save than with `start_offline.exe` (on the same device)
If you already had a save on SPNATI and you'd like it to be the same as the `start_offline.exe` save, then it's quite easy.  
- First, change the port from `5000` to `8080`  (in the .py file).
- Then, you must access SPNATI using the same URL as when using  `start_offline.exe` (most likely [`http://localhost:8080/`](http://localhost:8080/))  

This is because because which save is used is determined by the URL (the browser will choose the local storage depending on the URL, and local storage is where SPNATI saves are)