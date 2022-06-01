# Firefox 68.0 and the Offline Version

How to play SPNatI offline

---

## Why opening index.html directly does not work

If you attempt to load the offline version directly in most browsers, you will not be able to access any opponents, and some buttons may fail to display properly. Previously, Firefox was able to run the offline version via a `file://` URI (by opening `index.html` directly in the browser), but this was disabled some time ago.

Most browsers see a locally saved html file with the ability to access other files as a security threat. By default, they block html files on your hard drive from interacting with any other files on your computer.

---

## In-Browser Workaround, by /u/no_buggers

This solution does not require any server hosting.

Type `about:config` in the url bar,  and accept the warning (we won't break anything, I promise), then search `privacy.file_unique_origin` and set it to `false`. Next, search `security.fileuri.strict_origin_policy` and set it to `false` as well. This *should* allow access to local directories.

**DISCLAIMER:** Be aware, this was changed for a reason. Be smart and don't open random files from internet. A malicious HTML file can look through, and maybe even upload your personal files to a foreign server if you disable this.

!!! note
	If using the alternative workaround, know that this security risk only applies to html files downloaded to and run from your local hard drive. Websites on the internet still cannot access your local files with this method, even the shady Russian ones.
    
---

## Using Docker
This solution requires experience with the command lines for macOS, Linux, or WSL. If you are not experienced with Docker Desktop on Windows/macOS or Docker on Linux, consider the Local Server Workaround below.

We provide a Dockerfile to build and run SPNatI. We do not, however, host the built image for download.

Prerequisite: [Set up Docker](https://docs.docker.com/get-started/) for your OS.

1. Build a new version, navigate to your game files run: `docker build -t spnati:latest .`
2. Run the new container: `docker run --rm -it -p 8080:8080 spnati:latest`
3. Finally, simply navigate your web browser to `http://localhost:8080`

If you want to change the game files for development, mount the SPNatI files into the container: `docker run --rm -it -p 8080:8080 -v /path/to/game:/usr/share/nginx/html spnati:latest`

## Local Server Workaround

You can use a local webserver to access the offline version. Scripts and executables for starting webservers to access the offline version are included in the SPNatI repository.

[You can also download the Windows executable directly.](https://gitgud.io/spnati/spnati/raw/master/start_offline.exe)

[If you're on OSX or Linux, you can download a shell script to start the offline version here.](https://gitgud.io/spnati/spnati/raw/master/start_offline.sh?inline=false) Note that you'll need to [install NodeJS](https://nodejs.org/en/download/) before running this script.

For both of these downloads: just place them in your SPNATI directory and run it from there. It'll automatically start a local webserver and open a browser for you. You'll need to run it every time you play the offline version.

You can use any browser with these scripts. Simply run them and navigate to [http://localhost:8080](http://localhost:8080).

!!! note
	These local servers have issues with cached files not being cleared automatically. In order to see any changes made to your local repository, you'll need to first clear your chosen browser's cache.

---

## More information:

From security fixes introduced as a part of Firefox 68.0 (**Local files can no longer access other files in the same directory.**):

> [CVE-2019-11730]
> 
> Same-origin policy treats all files in a directory as having the same-origin
> 
> ---
> 
> A vulnerability exists where if a user opens a locally saved HTML file, this file can use file: URIs to access other files in the same directory or sub-directories if the names are known or guessed.  
> 
> The Fetch API can then be used to read the contents of any files stored in these directories and they may uploaded to a server.  
> 
> Luigi Gubello demonstrated that in combination with a popular Android messaging app, if a malicious HTML attachment is sent to a user and they opened that attachment in Firefox, due to that app's predictable pattern for locally-saved file names, it is possible to read attachments the victim received from other correspondents.  
