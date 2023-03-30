#!/bin/bash

dotnetver=7.0

sdkfile=/tmp/dotnetsdk.tar.gz
aspnetfile=/tmp/aspnetcore.tar.gz

download() {
    [[ $downloadspage =~ $1 ]]
    linkpage=$(wget -qO - https://dotnet.microsoft.com${BASH_REMATCH[1]})

    matchdl='id="directLink" href="([^"]*)"'
    [[ $linkpage =~ $matchdl ]]
    wget -O $2 "${BASH_REMATCH[1]}"
}

detectArch() {
    arch=x64
    if command -v uname > /dev/null; then
        machineCpu=$(uname -m)-$(uname -p)
        if [[ $machineCpu == *aarch*  && $machineCpu == *64* ]]; then
            arch=arm64
        fi
    fi
}


downloadspage=$(wget -qO - https://dotnet.microsoft.com/download/dotnet/$dotnetver)

detectArch

download 'href="([^"]*sdk-[^"/]*linux-'$arch'-binaries)"' $sdkfile

mkdir /opt/dotnet/

tar -xvf $sdkfile -C /opt/dotnet/

ln -s /opt/dotnet/dotnet /usr/bin/dotnet