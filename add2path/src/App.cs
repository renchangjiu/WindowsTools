using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace add2path;

public class App {

    private const string MENU_NAME = "添加到PATH";
    private const string INSTALL_CMD = "-add2PATH";
    private const string UNINSTALL_CMD = "-removeFromPATH";

    private readonly FileInfo iconFile;
    private readonly FileInfo exeFile;

    public static void Main(string[] args) {
        try {
            if (args.Length == 0) {
                Console.WriteLine("Error: no parameter.");
            }

            string param = args[0];
            var app = new App();
            switch (param) {
                case INSTALL_CMD:
                    app.addRightClickMenu();
                    break;
                case UNINSTALL_CMD:
                    app.removeRightClickMenu();
                    break;
                default:
                    app.addOrDeletePath(param);
                    break;
            }

            Thread.Sleep(1000);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
    }

    private App() {
        iconFile = new FileInfo(Environment.CurrentDirectory + "/app.ico");
        exeFile = new FileInfo(Environment.CurrentDirectory + "/add2path.exe");
    }

    private void addRightClickMenu() {
        string cmd = $"\"{exeFile.FullName}\" \"%v\"";
        RegeditUtil.add(MenuType.Directory, MENU_NAME, iconFile, cmd);
        RegeditUtil.add(MenuType.Background, MENU_NAME, iconFile, cmd);
        Console.WriteLine("添加到右键菜单成功");
    }

    private void removeRightClickMenu() {
        RegeditUtil.remove(MenuType.Directory, MENU_NAME);
        RegeditUtil.remove(MenuType.Background, MENU_NAME);
        Console.WriteLine("删除右键菜单成功");
    }

    private void addOrDeletePath(string path) {
        Console.WriteLine("正在读取Path......");
        string? oldPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        // Console.WriteLine(oldPath);
        // Environment.SetEnvironmentVariable("Path_Bak", oldPath, EnvironmentVariableTarget.User);
        string[] paths = oldPath.Split(";");
        HashSet<string> set = new();
        foreach (var item in paths) {
            if (!string.IsNullOrWhiteSpace(item)) {
                set.Add(item);
            }
        }

        // 已存在则移除, 否则加入
        if (set.Contains(path)) {
            set.Remove(path);
            Console.WriteLine($"已从Path中删除 {path}");
        } else {
            set.Add(path);
            Console.WriteLine($"已添加 {path} 到Path");
        }

        Console.WriteLine("正在修改Path......");
        string newPath = string.Join(";", set);
        Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);
        Console.WriteLine("Completed.");
    }


}