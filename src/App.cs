using System;
using System.Collections.Generic;
using System.Threading;

namespace win_rcmts;

public static class App {

    private const string INSTALL_CMD = "-add2Regedit";
    private const string UNINSTALL_CMD = "-removeFromRegedit";

    private static List<ITool> tools;


    public static void Main(string[] args) {
        try {
            if (args.Length == 0) {
                Console.WriteLine("Error: no parameter.");
                return;
            }

            string direct = args[0];
            tools = new List<ITool>();
            tools.Add(new AddPath());
            tools.Add(new CopyFilePath());
            // ...

            switch (direct) {
                case INSTALL_CMD:
                    addRightClickMenu();
                    break;
                case UNINSTALL_CMD:
                    removeRightClickMenu();
                    break;
                default:
                    invokeTool(direct, args[1]);
                    break;
            }
        } catch (Exception e) {
            Console.WriteLine(e);
        }
    }


    private static void addRightClickMenu() {
        string exe = Environment.CurrentDirectory + "/win-rcmts.exe";
        foreach (var tool in tools) {
            string direct = tool.getDirect();
            string cmd = $"\"{exe}\" {direct} \"%v\"";
            MenuType[] types = tool.GetMenuTypes();
            foreach (var type in types) {
                RegeditUtil.add(type, tool.getMenuName(), tool.getIconPath(), cmd);
            }
        }

        Console.WriteLine("添加到右键菜单成功");
        Thread.Sleep(1000);
    }

    private static void removeRightClickMenu() {
        foreach (var tool in tools) {
            MenuType[] types = tool.GetMenuTypes();
            foreach (var type in types) {
                RegeditUtil.remove(type, tool.getMenuName());
            }
        }

        Console.WriteLine("删除右键菜单成功");
        Thread.Sleep(1000);
    }

    private static void invokeTool(string direct, string param) {
        foreach (var tool in tools) {
            if (direct.Equals(tool.getDirect())) {
                tool.run(param);
                break;
            }
        }
    }

}