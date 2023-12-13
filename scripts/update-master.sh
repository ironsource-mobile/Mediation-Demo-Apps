#!/bin/bash

# Check out the master branch
git checkout master

# Reset the master branch to the origin's master
git reset --hard HEAD

# Fetch the remote repository
git fetch origin

# Compare your master with the origin's master
if [ `git rev-parse HEAD` != `git rev-parse origin/master` ]; then
    echo "Master is not up-to-date. Proceeding to update..."
    git pull origin master
    if [ $? -eq 0 ]; then
        echo "Master is now up-to-date."
    else
        echo "Failed to update master."
    fi
else
    echo "Master is up-to-date."
fi