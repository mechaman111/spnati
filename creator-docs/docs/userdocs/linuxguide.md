# Linux SPNatI Offline/Development Setup Guide

By no_buggers (V 1.0 2020-09-02)

---

So, you use Linux.  
Open source is your life.  
It powers your OS, your applications-  
...your porn.  
And being the good user that you are, you want to give back to this great project for all it's uh-  
*given* you.  
But alas! The development tools only work on Windows!  
Fear not, future contributor, for there is a better way.  
Almost 3 decades have been spent working by nerds, with the exact same problems you and I have.  
I am of course, talking about WINE, our secret weapon.  
This guide is to help all two people who use Linux, and don't know what they are doing, to get up and running.  
Yes, I do mean \*you\*, Josh.  

*why in the world did I spend so long writing this when literally no one is going to find this helpful*  

These instructions should cover most major distributions and their derivatives, but I have personally tested these instructions on:  

    Ubuntu 20.04 Tested 20th of July, 2020
    Debian 10    Tested 24th of July, 2020
    Fedora 32    Tested 21st of July, 2020
    Manjaro      Tested 23rd of July, 2020
    Arch         Tested 25th of July, 2020

**If you have any issues, questions, concerns, or whatever else, feel free to message me at  
u/no_buggers on reddit  
@​no\_buggers on the spnati discord  
There is also a** mostly **serious FAQ at the end of this post.**  

Quick note for newbs:  
`$ <command>` is to run a command as a non root user.  
`# <command>` is to run a command as a root user.  
Usually the latter is accomplished by running `sudo <command>`, but logging into root works as well (but don't do that, that is bad).  
If you are copy and pasting commands, do **NOT** copy the `$` or `#` symbol.  

# Step 0: Update your system.  

This isn't strictly required, but it is good practice to make sure your system is up to date before starting.  
Ubuntu/Debian/Mint: `# apt update` then `# apt upgrade`  
Fedora:             `# dnf upgrade`  
Arch/Manjaro:       `# pacman -Syu`  
Alternatively use whatever graphical software manager to update instead, you heckin' loser.  

# Step 1: Install Git  

"Oh no github desktop is only for Windows and Mac OS!!!111!11 What ever shall I do?"  
There probably exists some Linux alternative, but honestly, why even bother?  
We'll just do things in terminal, trust me it is much easier and faster this way.  

Install git, if you somehow don't have it installed already.  
Ubuntu/Debian/Mint  `# apt install git`  
Fedora              `# dnf install git`  
Arch/Manjaro        `# pacman -S git`  
(Or use your graphical software manager)  

SPNatI also uses git-lfs, or Large File Storage. We need to install this as well.  
This is less likely to be already installed.  
Ubuntu/Debian/Mint  `# apt install git-lfs`  
Fedora              `# dnf install git-lfs`  
Arch/Manjaro        `# pacman -S git-lfs`  
Next, run: `$ git lfs install.`  
Depending on your distribution, this step may or may not be necessary, but just run it to be safe.  

# Step 2: Cloning the Repo  

In your preferred directory run:  
`$ git clone` [`https://gitgud.io/spnati/spnati.git`](https://gitgud.io/spnati/spnati.git) ^(\(or whatever the new link is if we get booted off \*AGAIN\*\))  
This will probably take a bit. Go play a game of spnati or something while you wait you horny fucker.  
Once it is finished, the repo should be successfully cloned. But just to be sure, navigate into the newly spnati folder and run:  
`$ git pull`  
`$ git lfs pull` (This is also how you update the repo, but more on that later)    

# Step 3: Launching the Game  

There are two common ways of launching spnati  
The Webserver method: Uses whichever browser is associated with .html files, but starts a webserver every time you run the game, and requires the npm package. You run the game by running start\_offline.sh  
The Firefox method: Restricted to Firefox only. You run the game by opening index.html  

**Step 3A: Webserver Method**  
To run the start\_offline.sh script, we need npx, which is part of the npm package.  
Install npm:  
Ubuntu/Debian/Mint  `# apt install npm`  
Fedora              `# dnf install npm`  
Arch/Manjaro        `# pacman -S npm`  
Then just run `start_offline.sh`  
(You may need to make it as executable w/ `$ chmod +x ./start_offline.sh` , or opening the file's properties and ticking "executable" with your desktop environment)  

**Step 3B: Alternative Firefox Method**  
In the Firefox URL bar type `about:config`. It will show you a warning, but there is nothing to fear, we won't break anything.  
In the search bar, type `privacy.file.unique\_origin`. A single option should appear. Double click it to set it to FALSE.  
Changing this allows local html files (like spnati) to access resources (like lewd pictures) from the directory it is in.  
**Be aware, this was changed for a reason!**  
Be smart and don't open random files from internet. A malicious HTML file can look through, and maybe even upload your personal files to a foreign server if you disable this. Don't be stupid, and only run things you trust. (Like spnati)  
To launch the game, open index.html in Firefox.  

# Step 4: Updating the Repo  

To update the repo, navigate to the spnati folder, and run `$ git pull`  
Then, once that is done, run `$ git lfs pull`  
Once both are done, your offline install should now be updated to latest!  

**If you only wanted to setup spnati offline, you can stop here. Beyond this is just for setting up the developer tools- Kisekae and the Character Editor.**  

# Step 5: Installing WINE  

This is going to be the hardest part, and varies the most per distro.  
We will be using WINE, specifically, WINEHQ's release of it. If you want to know why, check the FAQ at the end.  
WINE stands for "Wine Is Not an Emulator", as instead of emulating windows, it translates system calls and whatnot to their Linux equivalents, to keep performance relatively high.  

**Ubuntu/Mint**  
Install the WineHQ packages, as explained here: [https://wiki.winehq.org/Ubuntu](https://wiki.winehq.org/Ubuntu)  
If you already have Ubuntu's wine package installed, uninstall it first.  
NOTE: Ubuntu 18.04/Mint 19.04 requires a dependency (FAudio) that is not in the default repo. Download and install both 32bit (i386) and 64bit (amd64) packages.  
Links for the packages are included with the [winehq.org](https://winehq.org) guide, however I have them here below for convenience. They may become out of date, so I still recommend going through winehq instead!  

`amd64`   [https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/xUbuntu\_18.04/amd64/libfaudio0\_19.07-0\~bionic\_amd64.deb](https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/xUbuntu_18.04/amd64/libfaudio0_19.07-0~bionic_amd64.deb)  

`i386`    [https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/xUbuntu\_18.04/i386/libfaudio0\_19.07-0\~bionic\_i386.deb](https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/xUbuntu_18.04/i386/libfaudio0_19.07-0~bionic_i386.deb)  

**Debian**  
Install the WineHQ packages, as explained here: [https://wiki.winehq.org/Debian](https://wiki.winehq.org/Debian)  
If you already have Debian's wine package installed, uninstall it first.  
NOTE: Debian 10 requires a dependency (FAudio) that is not in the default repo. Download and install both 32bit (i386) and 64bit (amd64) packages.  
Links for the packages are included with the [winehq.org](https://winehq.org) guide, however I have them here below for convenience. They may become out of date, so I still recommend going through winehq instead!  

`amd64`   [https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/Debian\_10/amd64/libfaudio0\_20.01-0\~buster\_amd64.deb](https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/Debian_10/amd64/libfaudio0_20.01-0~buster_amd64.deb)  

`i386`    [https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/Debian\_10/i386/libfaudio0\_20.01-0\~buster\_i386.deb](https://download.opensuse.org/repositories/Emulators:/Wine:/Debian/Debian_10/i386/libfaudio0_20.01-0~buster_i386.deb)

**Fedora**  
Install the WineHQ packages, as explained here: [https://wiki.winehq.org/Fedora](https://wiki.winehq.org/Fedora)  
If you already have Fedora's wine package installed, uninstall it first.  

**Manjaro**  
Just install wine, wine-mono, and wine-gecko. The multilib repo should be enabled by default. If not, follow the linked guide from the Arch instructions.  
`# pacman -S wine wine-mono wine-gecko`  

**Arch**  
Enable the multilib repo, if not already enabled <[https://wiki.archlinux.org/index.php/Official\_repositories#multilib](https://wiki.archlinux.org/index.php/Official_repositories#multilib)\>, then install wine, wine-mono, and wine-gecko.`# pacman -S wine wine-mono wine-gecko`  

The first time you run wine, it may ask about missing mono and gecko packages. If you are using WineHQ's packages (i.e. everyone but Arch/Manjaro), accept this.  
If you are getting this notice on Arch/Manjaro, make sure you actually installed wine-mono and wine-gecko, though downloading it with the popup won't hurt.  

# Step 6: Running Kisekae  

Kisekae is located in spnati/tools/kkl. Extract the latest version, and run kkl.exe with wine.  
Kisekae runs flawlessly in my experience, and runs on most installs of wine, no WineHQ packages required. Even your normal lame distro package of wine should work.  
Wine's file explorer sucks though, so opening your stuff might be a mildly frustrating.  

# Step 7: Running the Character Editor  

The Character Editor is located in spnati/tools/character\_editor. Extract the latest version and run SPNATI Character Editor.exe with wine.  
It should ask where your spnati repo, and where kkl.exe is.  
**Remember that your root directory is mounted as drive "Z"!**  
So if your repo is in your home folder, it would look like- `Z:\home\<your-user>\spnati\` and `Z:\home\<your-user>\spnati\tools\kkl\<kkl-ver>\kkl.exe`  
The character editor is not amazingly stable running this way. You may experience visual glitches, or it might even crash.  
Because of this, just in case, **SAVE OFTEN**.  
**Also, if you experience any "unhandled exception" errors, don't worry.  
Just dismiss them, and everything should keep running smoothly.**  
This is unfortunately the ways things are for now.  
But who knows, these issues may be fixed months from now in a future wine update.  

# FAQ  

**Q: <thing> didn't work for me, I need help! / I am confused!**  
*A: did you try turning it off and on again?*  
A: **If you need any help, feel free to message me, either on reddit as u/no_buggers or @​no\_buggers on the spnati discord.** I should get back to you within 24 hours.  
No matter how stupid the question or problem is, I'll try and help. *and only laugh a little*  

**Q: Something on this guide is incorrect / outdated / misleading / confusing!**  
*A: that's not a question, stupid*  
A: If you think anything can be improved on this guide, contact me, and I'll try to fix it.  
I only daily drive Arch Linux ~~btw i use arch~~, so I cannot always be sure everything works all the time.  

**Q: Why do you recommend using WineHQ packages, instead of adding wine tricks?**  
A: Although it is possible to get the character editor working on most distributions with only their default wine package, I personally believe it is an inferior setup.  

For example, in Debian 10, to get the character editor working without wineHQ's packages, you must use the winetricks script, and then install over half dozen libraries to your wine prefix (the .wine folder in your home folder.) The lateast wineHQ package does not need these extra steps, and "just works" out of the box.  

Additionally, If, for any reason you have to wipe your wine prefix, you'll have to reinstall all of those libraries.  

Even then, with everything working, you are still running an outdated version of wine, and if any improvements are made to wine, that, perhaps prevents the CE from crashing randomly, you will not benefit from it, since Debian's wine package is outdated.  

Setting up the WineHQ repo takes about as much time as installing all the winetricks libraries, and provides less work later on, and provides access to the newest wine updates regardless of your distro.  

**Q: Why did write this? Literally no one uses Linux, you freaking loser.**  
*A: I hate myself, that's why.*    
A: There are more linux users out there than you think! And I wanted to make sure there were easy instructions for them. Even if I only help a few people, that's enough.  
*And also I'm a masochist and like torturing myself*  

**Q: Can you make a guide for BSD too?**  
A: Why would I write that? Literally no one uses BSD, you freaking loser.