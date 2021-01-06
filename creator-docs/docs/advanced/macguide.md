# How to use SPNatI Dev Tools on Mac OS

*By Thevideogameraptorboggle*

---

## Intro

Since the game's inception, we have gotten a number of tools to make character creation far easier, and that is great. "But Mr. Raptor", you say in a small voice, "I am on MacOS, and cannot use all these wonderful tools." And I say to you, "There are many ways to trick programs into thinking they're running on a Windows computer, so you can use all our wonderful tools without a Windows PC, or even without Bootcamp for that matter."

As always, all dev tools are included as part of the [SPNatI Git Repository](https://gitgud.io/spnati/spnati). This guide assumes you've already downloaded the latest version.

!!! Note
    This guide is intended for those who have not yet updated to **Mac OS Catalina** or later. Other methods might be available for operating the SPNatI dev tools on 64-bit only systems, however those are still being looked at as of the time of this writing.

## Kisekae (KKL)

KKL is the easier of the two to get running. Just download [Wine](https://www.winehq.org/) (make sure not to grab a tarball). Once installed, right click on the `kkl.exe` executable to run it using wine. Easy!

You can also use the same method as below, only using the `adobeair` Winetrick.

## Character Editor

The character editor is somewhat more complicated to get running, requires a few more steps than Kisekae. Basic wine will not run the character editor without a significant number of workarounds. Instead, use Winebottler to create a Wine environment specifically for the CE.

First, download and install [Winebottler](https://winebottler.kronenberg.org/).

1.86 is fine, or whatever most recent stable version they have.

Go to the Advanced section of Winebottler.

Put the character editor executable (`SPNATI Character Editor.exe`)in the Program installation box.

Check off "Copy program and all files in the folder to the app bundle".

There will be a box that will let you add what are called "Winetricks". These are essentially additional programs that help you run your main program.

Add the following Winetricks:

 - `dotnet46`
 - `msls31`
 - `msxml6`
 - `python26`
 - `updspapi`
 - `vb6run`
 - `vcrun2017`
 - `ie8`

Just agree to every **EULA** and **Restart Now** prompt it gives you. It doesn't actually restart your computer, only the Wine instance.

One important tip, do not, I repeat, DO NOT, set the app bundle to be installed in the same folder as the character editor. They have to be separate for it to work.

And also, if the editor doesn't work the first time you open it, that is normal. Especially if the error reads "X number of characters failed to load". It should work fine if you close and reopen it.