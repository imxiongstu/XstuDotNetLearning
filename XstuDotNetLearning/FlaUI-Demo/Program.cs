using FlaUI.Core;
using FlaUI.UIA3;
using System.Diagnostics;

var processes = Process.GetProcessesByName("WeChat");

using (var app = Application.Attach(processes.First().Id))
{
    using (var automation = new UIA3Automation())
    {
        var mainWindow = app.GetMainWindow(automation);
    }
}