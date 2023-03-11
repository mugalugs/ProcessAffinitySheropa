# ProcessAffinitySheropa
Simple program to control process affinity, select process, set affinity, update to save to config.json, apply mask to all running processes. Option to auto apply every 5 seconds, minimise to tray, close to close. Has -startMinimised and -autoApply argument options for windows startup shortcuts.

Made due to using an AMD X3D CPU (7900X3D) and wanted the ability to select the CCD for a process to use. Hope in future to add the ability to profile CPUs and then provide preferences based on cache, frequency, performance, effeciency cores etc.

For anticheat like EAC just set the affinity on the launcher, as the game process itself inherits the affinity of the launcher.

AffinitySherpa (use this one) is WinForms, made after the MAUI experiment. ProcessAffinitySherpa is the MAUI experiement and discontinued based on the fact that side loading without installing is currently not supported by MS.

![Example of all of the windows on the same screen](https://github.com/mugalugs/ProcessAffinitySheropa/raw/master/screenshots/winforms.png)