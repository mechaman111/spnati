#!/usr/bin/bash

# Takes a list of merge request numbers on the command line, merges them, and
# prepares a commit message containing a shortlog of all commits to be merged.
#
# Merges are performed as non-fastforward octopus merges, so no two heads
# should touch the same set of files.
# The commit message will be opened for editing prior to actually committing.
#
# This script assumes that the 'upstream' remote is configured to point to
# the upstream repository, where merge requests are submitted.

joined=$(printf ", !%s" "$@")

# Ensure we're working with the latest upstream MR / branch heads.
git fetch upstream '+refs/heads/*:refs/remotes/upstream/*' '+refs/merge-requests/*/head:refs/remotes/upstream/merge-requests/*'

# For some reason, git-merge doesn't support reading commit messages from stdin via '-F -'?
git merge --no-ff --no-commit "${@/#/upstream\/merge-requests\/}"
{ echo -e "Pull merge requests ${joined:2}\n" && git shortlog "${@/#/upstream\/master..upstream\/merge-requests\/}"; } | git commit -e --file=-

