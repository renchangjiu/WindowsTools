namespace win_rcmts;

public class MenuType {

    public static readonly MenuType File = new MenuType(@"*\shell");
    public static readonly MenuType Directory = new MenuType(@"Folder\shell");
    public static readonly MenuType Background = new MenuType(@"Directory\Background\shell");


    public string regeditPath;

    private MenuType(string regeditPath) {
        this.regeditPath = regeditPath;
    }

}