# UR Dashboard SDK for CB-Series Only

This article explains how use this UR Dashboard SDK to control the robot from remote control via Dashboard Server.

This SDK is developed based on Universal Robot Dashboard server, please refer to: https://www.universal-robots.com/articles/ur-articles/dashboard-server-cb-series-port-29999/

Please refer to protocol: https://s3-eu-west-1.amazonaws.com/ur-support-site/15690/DASHBOARD%20SERVER%20CB-SERIES,%20PORT%2029999.pdf

## 1. Add Reference to project

Add "URDashboardLibrary.dll" and "URSocketLibrary.dll" reference to your project.

## 2. Connect to UR

First, confirm UR's ip address.

Second, use `IURDashboardConnection.Create()` to create  an object for URDashboard.

Third, you will receive connected message from UR: "Connected: Universal Robots Dashboard Server".

```c#
// 1. confirm UR ip address
IpAddress = "192.168.245.129";
// 2. create urDashboard object
IURDashboard urDashboard = IURDashboardConnection.Create(IpAddress);
// 3. Read connection message
var resp = urDashboard.Receive();
```

## 3. Send Command to UR

You could get a full list of Dashboard Server commands from [protocol](https://s3-eu-west-1.amazonaws.com/ur-support-site/15690/DASHBOARD%20SERVER%20CB-SERIES,%20PORT%2029999.pdf), and use `urDashboard.SendReceive()` to send command to UR and receive response message, for example:

```c#
// play program command
string cmd = "play\n";
var resp = urDashboard.SendReceive(cmd);
```

Or, you could get command from ICommand class, for example:

```c#
// Get Serial Number command
var cmd = ICommand.GetSerialNumber();
// Send command
var resp = urDashboard.SendReceive(cmd);
```

## 4. WPF Sample Code

Here is a WPF sample project.