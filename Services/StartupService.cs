using Microsoft.Win32;

namespace WangzhenWorkbench.Services;

public static class StartupService
{
    private const string KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "WangzhenWorkbench";

    public static bool IsEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(KeyPath, false);
        return key?.GetValue(ValueName) is string value && !string.IsNullOrWhiteSpace(value);
    }

    public static bool Toggle()
    {
        var next = !IsEnabled();
        using var key = Registry.CurrentUser.OpenSubKey(KeyPath, true)
                       ?? throw new InvalidOperationException("无法打开 Windows 启动项注册表。");

        if (next)
        {
            var exe = Environment.ProcessPath ?? throw new InvalidOperationException("无法获取程序路径。");
            key.SetValue(ValueName, $"\"{exe}\"");
        }
        else
        {
            key.DeleteValue(ValueName, false);
        }
        return next;
    }
}
