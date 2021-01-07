# Keep Your Offline Version Up To Date With GitHub Desktop

Whether you're a developer or a player, stay always up to date!

---

Downloading the bundles for for Strip Poker Night at the Inventory is a great way to try out retired characters and to test your own characters in development. However, because the game receives tweaks and improvements daily, your offline version soon gets out of date, and re-downloading the bundles to replace your old files is upwards of a gigabyte.

The solution to this problem is to use GitHub Desktop, a program available for Windows and Mac. After the initial download, this will check for changed files and download *only the changes*.

Note that even though this is a GitHub client, it works with GitGud too.

---

## Setting up GitHub Desktop

### Step 1

Go to [desktop.github.com](https://desktop.github.com/) and **download the program**.

![githubdesktop_01.png](../img/githubdesktop_01.png "download the program")

---

### Step 2

Install GitHub Desktop.
When **it asks you to sign in to GitHub**, skip this step.

![githubdesktop_02.png](../img/githubdesktop_02.png "it asks you to sign in to GitHub")

**The information you supply on the Configure Git screen** will be used to identify you if  you [create a manual merge request](/docs/advanced/githubmerge.html).  Otherwise, it doesn't matter. You can just use the program to synchronize with the hosted version and not push commits through it.

![githubdesktop_03.png](../img/githubdesktop_03.png "The information you supply on the Configure Git screen")

!!! note
	The name and email you submit in the configure git screen will be publicly viewable **if and only if** you submit a manual merge request to the main SPNatI repository. However, in the GitGud settings, you can set your "Commit email" to "Use a private email" instead to hide it.

---

### Step 3

Go to [gitgud.io/spnati/spnati](https://gitgud.io/spnati/spnati) and **copy the HTTPS address** found when you press the blue "Clone" button.

Or just copy the link here: `https://gitgud.io/spnati/spnati.git`

![githubdesktop_04.png](../img/githubdesktop_04.png "copy the HTTPS address")

---

### Step 4

When you start GitHub Desktop for the first time, it will ask you what repository you want to add. **Choose "Clone a Repository from the Internet...", navigate to the "URL" tab, and paste the HTTPS address you copied into the URL field.**. You can also specify where you want this new copy of the game's files to be downloaded to.

![githubdesktop_05.png](../img/githubdesktop_05.png "Choose Clone a Repository from the Internet...")

---

### Step 5

The game's files will now download. Note that this includes every version of all .xml files since we moved to GitLab (even before our current host on GitGud), so this is currently a 5+ gigabyte download. But it will be worth it in the long run.

(It could be worse, prior to the move to Gitgud, you had to download every version of every *image* as well.)

---

### Step 6

At the end, GitHub Desktop may **prompt you to install Git LFS**. Do this by clicking OK.

![githubdesktop_06.png](../img/githubdesktop_06.png "prompt you to install Git LFS")

!!! note
	Git-LFS needs to be initialized in order to properly view all files. If GitHub Desktop does not prompt you to install, you will have to do it manually. [Follow this guide to do so](https://github.com/git-lfs/git-lfs/wiki/Installation) (requires use of a **Command Line Interface (CLI)**).  
	Right-clicking the Repository in Github Desktop and selecting "Remove..." without selecting "Move to Trash" will allow you to re-add that Repository by clicking and dragging the folder, at which point you will be prompted to install git-lfs again.

---

### Step 7

Now that the download is complete, you're all set up! Click the **Fetch origin** button to check for updates. If it finds any, the button will let you click it a second time to **pull the updates** to your computer.

![githubdesktop_07.png](../img/githubdesktop_07.png "Fetch origin")

![githubdesktop_07A.png](../img/githubdesktop_07A.png "pull the updates")

You will be given a warning if any of the files on your computer that differ from the online version are being changed in the update you're pulling. This is most likely to happen if you've modified `listing.xml` or `config.xml` for testing. Right-click your file in the listing and choose "discard changes" to restore a file you've changed to its original state. (Caution: This moves your changes to the Trash!)

---

## Features

### Local Diffs

You can see at a glance **how your local modifications differ from the hosted version**. This is helpful for developing your characters.

![githubdesktop_08.png](../img/githubdesktop_08.png "how your local modifications differ from the hosted version")

---

### Historical Diffs

You can also check the History tab to see **what changes have been made** to the files throughout time. This is very helpful for keeping up with changes made by others.

![githubdesktop_09.png](../img/githubdesktop_09.png "what changes have been made")

Only the Game Dev Mods can actually use the "Commit to master" button to push their changes directly to the hosted version, so you will still have to use your regular method of Merge Requests for this. However, having a tool to easily update your local version should make keeping up with the game's changes significantly easier.

---

## Warning: Working on a Character With a Pending Merge Request

When you have uploaded changes to a character, before pulling changes from the main repository and discarding your own changes, always click the link to the Merge Request and check that it has been accepted.

If you forget, and only realize that your previous changes are missing after you have started making new changes, **don't panic!** Don't start re-doing the previous changes. Instead, stop and contact a Mod to get help combining your changes, which can likely save a lot of time.