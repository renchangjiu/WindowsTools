using System;
using System.IO;
using Microsoft.Win32;

namespace add2path;

public static class RegeditUtil {

    public static void add(MenuType type, string menuName, FileInfo icon, string command) {
        RegistryKey shellKey = getShellKey(type);

        RegistryKey menuKey = shellKey.CreateSubKey(menuName, true);
        menuKey.SetValue("icon", icon.FullName, RegistryValueKind.String);
        RegistryKey cmdKey = menuKey.CreateSubKey("command", true);
        cmdKey.SetValue("", command, RegistryValueKind.String);
        menuKey.Close();
    }

    public static void remove(MenuType type, string menuName) {
        RegistryKey shellKey = getShellKey(type);
        shellKey.DeleteSubKeyTree(menuName);
        shellKey.Close();
    }

    private static RegistryKey getShellKey(MenuType type) {
        RegistryKey root = Registry.ClassesRoot;
        return root.OpenSubKey(type.regeditPath, true);
    }

}