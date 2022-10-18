using System;
using System.Collections.Generic;

namespace win_rcmts;

public class AddPath : ITool {

    public void run(string param) {
        Console.WriteLine("正在读取Path......");
        string? oldPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        string[] paths = oldPath.Split(";");
        HashSet<string> set = new();
        foreach (var item in paths) {
            if (!string.IsNullOrWhiteSpace(item)) {
                set.Add(item);
            }
        }

        // 已存在则移除, 否则加入
        if (set.Contains(param)) {
            set.Remove(param);
            Console.WriteLine($"已从Path中删除 {param}");
        } else {
            set.Add(param);
            Console.WriteLine($"已添加 {param} 到Path");
        }

        Console.WriteLine("正在修改Path......");
        string newPath = string.Join(";", set);
        Environment.SetEnvironmentVariable("Path", newPath, EnvironmentVariableTarget.User);
        Console.WriteLine("Completed.");
    }

    public MenuType[] GetMenuTypes() {
        return new[] { MenuType.Background, MenuType.Directory };
    }

    public string getMenuName() {
        return "R: 添加到PATH";
    }

    public string getDirect() {
        return "-addOrRemovePath";
    }

    public string getIconPath() {
        return Environment.CurrentDirectory + "/resources/app2.ico";
    }

}