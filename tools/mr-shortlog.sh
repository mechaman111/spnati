#!/usr/bin/bash

# Takes a list of merge request numbers on the command line and displays
# a shortlog of all commits within the specified merge requests.

git shortlog "${@/#/upstream\/master..upstream\/merge-requests\/}"

