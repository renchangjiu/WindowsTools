using System;
using System.Runtime.InteropServices;

namespace win_rcmts;

public class CopyFilePath : ITool {

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SetClipboardData(uint format, IntPtr hData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool CloseClipboard();

    private const uint CF_UNICODETEXT = 13;

    private static void setClipboardText(string text) {
        OpenClipboard(IntPtr.Zero);
        EmptyClipboard();
        SetClipboardData(CF_UNICODETEXT, Marshal.StringToHGlobalUni(text));
        CloseClipboard();
    }

    public void run(string param) {
        param = param.Replace("\\", "/");
        setClipboardText(param);
    }

    public MenuType[] GetMenuTypes() {
        return new[] { MenuType.File, MenuType.Directory, MenuType.Background };
    }

    public string getMenuName() {
        return "R: 复制文件路径";
    }

    public string getDirect() {
        return "-copyFilePath";
    }

    public string getIconPath() {
        return Environment.CurrentDirectory + "/resources/app1.ico";
    }

}