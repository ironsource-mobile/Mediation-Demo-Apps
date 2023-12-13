#!/bin/bash
detectOS() {
    unset OS
    if [[ "$(uname)" == "Darwin" ]]; then
        OS="Mac"
    elif [[ "$(uname)" == "Linux" ]]; then
        OS="Linux"
    else
        echo "Unsupported OS. Please use Linux or Mac."
        exit 1
    fi
}

function setSdkVersion() {
  local sdkVersion="$1" # update as needed
  local gradleFilePath=$2
  local osType=$(uname)
  echo "osType: "$osType
  echo "new sdkVersion: "$sdkVersion
  echo "file Path: "$gradleFilePath

  if [[ "$osType" == "Linux" ]]; then
    sed -i 's/sdkVersion = .*/sdkVersion = "'$sdkVersion'"/' "$gradleFilePath"
  elif [[ "$osType" == "Darwin" ]]; then
    sed -i '' 's/sdkVersion = .*/sdkVersion = "'$sdkVersion'"/' "$gradleFilePath"
  else
    echo "Unknown operating system."
    return 1
  fi
}

if [ -z "$1" ]; then
    echo "Please provide a version number for release."
    exit 1
fi

detectOS

export RELEASE_BRANCH="release/$RELEASE_VERSION"
export SDK_VERSION="$1" #New SDK version to be updated
export GIT_SHA="`git log --pretty=format:'%h' -n 1`"


# Checkout to master
#bash ./scripts/update-master.sh

# Create release branch checkout
#git checkout -b $RELEASE_BRANCH

# Update SDK version
setSdkVersion "$SDK_VERSION" ./Android/Java/app/build.gradle
setSdkVersion "$SDK_VERSION" ./Android/Kotlin/app/build.gradle

# Commit changes
git add .
git commit -m "Release: $RELEASE_BRANCH commit: $GIT_SHA"
#git push origin $RELEASE_BRANCH