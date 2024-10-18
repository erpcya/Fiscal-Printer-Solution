# Fiscal Printer Runner

All docs can be downloaded from [docs](docs/)

## Install Need
https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2004-

## Build it
```
dotnet build
```

## Clean it
```
dotnet clean
```

## Build for a specific runtime

- Linux x64
```
dotnet build --runtime linux-x64
```
- Windows 86
```
dotnet build --runtime win-x86
```

## Build for a specific runtime and save build log
```
dotnet build -r win-x86 /flp:v=diagag
```

## run it
```
dotnet run "COM1" "/tmp/file.txt"
```

## run it for specific OS

- Linux x86
```
dotnet run --runtime linux-x64 "COM1" "/tmp/file.txt"
```
- Window
```
dotnet run --runtime win-x86 "COM1" "/tmp/file.txt"
```

### Clean
```Shell
dotnet nuget locals all --clear
```